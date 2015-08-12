using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using Neat.Infrastructure.ObjectCreation;
using Neat.Infrastructure.Security.Attribute;
using Neat.Infrastructure.Security.Context;
using Neat.Infrastructure.Security.Model.Response;

namespace Neat.Infrastructure.Security
{
    public class SecurityAuthorizationProvider : ISecurityAuthorizationProvider
    {
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;
        private readonly ISecurityACLProvider _securityACLProvider;
        private readonly ISecurityPermissionProvider _securityPermissionProvider;
        private readonly ISecurityContext _securityContext;
        private readonly IObjectFactory _objectFactory;

        public SecurityAuthorizationProvider(ISecurityAccessTokenProvider securityAccessTokenProvider, ISecurityACLProvider securityACLProvider, ISecurityPermissionProvider securityPermissionProvider, ISecurityContext securityContext, IObjectFactory objectFactory)
        {
            _securityAccessTokenProvider = securityAccessTokenProvider;
            _securityACLProvider = securityACLProvider;
            _securityPermissionProvider = securityPermissionProvider;
            _securityContext = securityContext;
            _objectFactory = objectFactory;
        }

        public AuthorizationResponse GetAuthorizationForAccessToken(string accessToken)
        {
            var user = _securityAccessTokenProvider.GetUserFromAccessToken(accessToken);
            var authorizationResponse = new AuthorizationResponse();
            authorizationResponse.IsAuthorized = user != null;

            if (user != null)
            {
                authorizationResponse.UserId = user.Id;
            }
            
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Session Has Expired Or User is Not Logged In!");
            }

            return authorizationResponse;
        }

        public AuthorizationResponse CheckUserAuthorization(string action)
        {
            var authorizationResponse = new AuthorizationResponse();
            var role = _securityACLProvider.GetCurrentUserRole();
            var actions = _securityPermissionProvider.GetActionsForRole(role);

            authorizationResponse.IsAuthorized = actions.Contains(action);
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Current User with Role {0} does not have permission to perform Action {1}!", role, action);
            }

            return authorizationResponse;
        }

        public AuthorizationResponse CheckObjectAuthorization(object securedObject, object originalObject, string action)
        {
            var authorizationResponse = new AuthorizationResponse();
            var role = _securityACLProvider.GetCurrentUserRoleForObject(securedObject);
            var actions = _securityPermissionProvider.GetActionsForRole(role);
                
            authorizationResponse.IsAuthorized = actions.Contains(action);
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Current User with Role {0} for Object does not have permission to perform Action {1} for current Object!", role, action);
            }

            if (_securityContext.EnableFieldLevelSecurity)
            {
                var fieldLevelAuthorizationResponse = ProcessObjectFieldSecurity(securedObject, originalObject, action, role);
                authorizationResponse.SecuredObject = fieldLevelAuthorizationResponse.SecuredObject;
                authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, fieldLevelAuthorizationResponse.AuthorizationMessage);
            }

            return authorizationResponse;
        }

        private AuthorizationResponse ProcessObjectFieldSecurity(object securedObject, object originalObject, string action, string role, int depth = 0)
        {
            var authorizationResponse = new AuthorizationResponse();
            if (securedObject == null)
            {
                return authorizationResponse;
            }
            if (originalObject == null)
            {
                throw new SecurityException("No Secure State of Object Exists!");
            }
            var authorizationFailureMessages = new List<string>();
            var securedType = securedObject.GetType();
            var securedProperties = securedType.GetProperties();
            var originalType = originalObject.GetType();
            var originalProperties = originalType.GetProperties();
            for (var i = 0; i < securedProperties.Length; i++)
            {
                var securedPropertyInfo = securedProperties[i];
                var securedPropertyType = securedPropertyInfo.PropertyType;
                var originalPropertyInfo = originalProperties[i];
                var securedPropertyInfoCustomAttributes = securedPropertyInfo.CustomAttributes;
                var securedValue = securedPropertyInfo.GetValue(securedObject, null);
                if (securedValue != null)
                {
                    var originalValue = originalPropertyInfo.GetValue(originalObject, null);
                    CustomAttributeData secureAttribute;
                    if (action != "Read")
                    {
                        secureAttribute = securedPropertyInfoCustomAttributes.FirstOrDefault(x => x.AttributeType == typeof (SecureWritePropertyAttribute));
                    }
                    else
                    {
                        secureAttribute = securedPropertyInfoCustomAttributes.FirstOrDefault(x => x.AttributeType == typeof (SecureReadPropertyAttribute));
                    }
                    if (securedPropertyType.FullName != "System.String" && securedPropertyType.GetInterface("IEnumerable") != null)
                    {
                        var recursiveAuthorizationResponse = ProcessEnumerable(securedPropertyType, securedObject, originalObject, securedPropertyInfo, originalPropertyInfo, action, role, depth);
                        securedPropertyInfo.SetValue(securedObject, recursiveAuthorizationResponse.SecuredObject);
                        authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, recursiveAuthorizationResponse.AuthorizationMessage);
                    }
                    if (!(securedPropertyType.IsPrimitive || securedPropertyType.IsValueType || (securedPropertyType == typeof (string)) || securedPropertyType.GetInterface("IEnumerable") != null) && depth < _securityContext.FieldLevelSecurityEvaulationDepth)
                    {
                        var recursiveAuthorizationResponse = ProcessObjectFieldSecurity(securedValue, originalValue, action, role, depth++);
                        securedPropertyInfo.SetValue(securedObject, recursiveAuthorizationResponse.SecuredObject);
                        authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, recursiveAuthorizationResponse.AuthorizationMessage);
                    }
                    if (secureAttribute != null)
                    {
                        var securedPropertyName = secureAttribute.NamedArguments.FirstOrDefault(x => x.MemberName == "PropertyName").TypedValue.Value as string;
                        if (securedPropertyName == null)
                        {
                            securedPropertyName = string.Format("{0}.{1}", securedType.FullName, securedPropertyInfo.Name);
                        }

                        if (securedValue != originalValue && !_securityPermissionProvider.CanPerformActionOnProperty(role, action, securedPropertyName))
                        {
                            authorizationFailureMessages.Add(string.Format("Current User with Role {0} Does Not Have Permission to Perform Action {1} on Property {2}", role, action, securedPropertyName));
                            securedValue = originalValue;
                        }
                        securedPropertyInfo.SetValue(securedObject, securedValue);
                    }
                }
            }

            authorizationResponse.SecuredObject = securedObject;
            var authorizationMessageStringBuilder = new StringBuilder();
            foreach (var authorizationFailureMessage in authorizationFailureMessages)
            {
                authorizationMessageStringBuilder.Append(authorizationFailureMessage);
                authorizationMessageStringBuilder.Append(" ");
            }
            authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, authorizationMessageStringBuilder);

            return authorizationResponse;
        }

        private AuthorizationResponse ProcessEnumerable(Type securedPropertyType, object securedObject, object originalObject, PropertyInfo securedPropertyInfo, PropertyInfo originalPropertyInfo, string action, string role, int depth = 0)
        {
            var authorizationResponse = new AuthorizationResponse();
            if (securedObject == null)
            {
                return authorizationResponse;
            }
            if (originalObject == null)
            {
                throw new SecurityException("No Secure State of Object Exists!");
            }
            var securedValue = securedPropertyInfo.GetValue(securedObject, null);
            if (securedValue == null)
            {
                return authorizationResponse;
            }
            var originalValue = originalPropertyInfo.GetValue(originalObject, null);
            if (securedPropertyType.FullName != "System.String" && securedPropertyType.GetInterface("IEnumerable") != null)
            {
                var securedEnumerable = securedValue as IEnumerable;
                var originalEnumerable = originalValue as IEnumerable;
                var securedList = new List<object>();
                var originalList = new List<object>();
                foreach (var item in securedEnumerable)
                {
                    securedList.Add(item);
                }
                if (originalEnumerable != null)
                {
                    foreach (var item in originalEnumerable)
                    {
                        originalList.Add(item);
                    }
                }

                for (var j = 0; j < securedList.Count; j++)
                {
                    var securedCollectionItem = securedList[j];
                    var securedCollectionItemType = securedCollectionItem.GetType();
                    object originalCollectionItem = null;
                    if (originalList.Count > j)
                    {
                        originalCollectionItem = originalList[j];
                    }
                    else
                    {
                        originalCollectionItem = _objectFactory.Create(securedCollectionItemType);
                    }
                    var originalCollectionItemType = originalCollectionItem.GetType();
                    // TODO: Test List<List<object>>
                    if (securedCollectionItemType.FullName != "System.String" &&
                        securedCollectionItemType.GetInterface("IEnumerable") != null)
                    {
                        var securedCollectionItemPropertyInfo = securedCollectionItemType.GetProperty("Item");
                        var originalCollectionItemPropertyInfo = originalCollectionItemType.GetProperty("Item");
                        var recursiveAuthorizationResponse = ProcessEnumerable(securedCollectionItemType, securedCollectionItem, originalCollectionItem, securedCollectionItemPropertyInfo, originalCollectionItemPropertyInfo, action, role, depth);
                        securedList[j] = recursiveAuthorizationResponse.SecuredObject;
                        authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, recursiveAuthorizationResponse.AuthorizationMessage);
                    }
                    if (!(securedCollectionItemType.IsPrimitive || securedCollectionItemType.IsValueType || (securedCollectionItemType == typeof(string))) && depth < _securityContext.FieldLevelSecurityEvaulationDepth)
                    {
                        var newDepth = depth + 1;
                        var recursiveAuthorizationResponse = ProcessObjectFieldSecurity(securedCollectionItem, originalCollectionItem, action, role, newDepth);
                        securedList[j] = recursiveAuthorizationResponse.SecuredObject;
                        authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, recursiveAuthorizationResponse.AuthorizationMessage);
                    }
                }
            }
            authorizationResponse.SecuredObject = securedValue;

            return authorizationResponse;
        }

        public string GetAccessTokenForLoggedInUser(string userId)
        {
            return _securityAccessTokenProvider.GetAccessToken(userId);
        }
    }
}
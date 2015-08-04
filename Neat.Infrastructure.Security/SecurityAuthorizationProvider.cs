using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public SecurityAuthorizationProvider(ISecurityAccessTokenProvider securityAccessTokenProvider, ISecurityACLProvider securityACLProvider, ISecurityPermissionProvider securityPermissionProvider, ISecurityContext securityContext)
        {
            _securityAccessTokenProvider = securityAccessTokenProvider;
            _securityACLProvider = securityACLProvider;
            _securityPermissionProvider = securityPermissionProvider;
            _securityContext = securityContext;
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
                var fieldLevelAuthorizationResponse = ProcessObjectFieldSecurity(securedObject, originalObject);
                authorizationResponse.SecuredObject = fieldLevelAuthorizationResponse.SecuredObject;
                authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, fieldLevelAuthorizationResponse.AuthorizationMessage);
            }

            return authorizationResponse;
        }

        private AuthorizationResponse ProcessObjectFieldSecurity(object securedObject, object originalObject, int depth = 0)
        {
            var authorizationResponse = new AuthorizationResponse();
            var authorizationFailureMessages = new List<string>();
            if (securedObject == null)
            {
                return authorizationResponse;
            }
            var securedType = securedObject.GetType();
            var securedProperties = securedType.GetProperties();
            if (originalObject != null)
            {
                var originalType = originalObject.GetType();
                var originalProperties = originalType.GetProperties();
                for (var i = 0; i < securedProperties.Length; i++)
                {
                    var securedPropertyInfo = securedProperties[i];
                    var originalPropertyInfo = originalProperties[i];
                    var securedPropertyInfoCustomAttributes = securedPropertyInfo.CustomAttributes;
                    var secureWriteAttribute = securedPropertyInfoCustomAttributes.FirstOrDefault(x => x.AttributeType == typeof (SecureWritePropertyAttribute));
                    if (secureWriteAttribute != null)
                    {
                        var securedValue = securedPropertyInfo.GetValue(securedObject, new object[] { });
                        var originalValue = originalPropertyInfo.GetValue(originalObject, new object[] { });
                        var securedPropertyName = secureWriteAttribute.NamedArguments.FirstOrDefault(x => x.MemberName == "PropertyName").TypedValue.Value as string;
                        if (securedPropertyName == null)
                        {
                            securedPropertyName = string.Format("{0}.{1}", securedPropertyInfo.PropertyType.FullName, securedPropertyInfo.Name);
                        }
                        var role = _securityACLProvider.GetCurrentUserRoleForObject(securedObject);
                        var propertyType = securedPropertyInfo.PropertyType;
                        if (securedValue != originalValue && !_securityPermissionProvider.CanWriteToProperty(role, securedPropertyName))
                        {
                            authorizationFailureMessages.Add(string.Format("Current User with Role {0} does not have Permission to Write to Property {1}", role, securedPropertyName));
                            securedValue = originalValue;
                        }
                        securedProperties[i].SetValue(securedObject, securedValue);
                        if (!(propertyType.IsPrimitive || propertyType.IsValueType || (propertyType == typeof(string))) && depth < 10)
                        {
                            securedObject = ProcessObjectFieldSecurity(securedValue, originalValue, depth++);
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < securedProperties.Length; i++)
                {
                    var securedPropertyInfo = securedProperties[i];
                    var securedPropertyInfoCustomAttributes = securedPropertyInfo.CustomAttributes;
                    var secureReadAttribute = securedPropertyInfoCustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SecureReadPropertyAttribute));
                    if (secureReadAttribute != null)
                    {
                        var securedValue = securedPropertyInfo.GetValue(securedObject, new object[] { });
                        var securedPropertyName = secureReadAttribute.NamedArguments.FirstOrDefault(x => x.MemberName == "PropertyName").TypedValue.Value as string;
                        if (securedPropertyName == null)
                        {
                            securedPropertyName = string.Format("{0}.{1}", securedPropertyInfo.PropertyType.FullName, securedPropertyInfo.Name);
                        }
                        var role = _securityACLProvider.GetCurrentUserRoleForObject(securedObject);
                        var propertyType = securedPropertyInfo.PropertyType;
                        if (!_securityPermissionProvider.CanReadFromProperty(role, securedPropertyName))
                        {
                            authorizationFailureMessages.Add(string.Format("Current User with Role {0} does not have Permission to Read to Property {1}", role, securedPropertyName));
                            securedPropertyInfo.SetValue(securedObject, null, null);
                        }
                        if (!(propertyType.IsPrimitive || propertyType.IsValueType || (propertyType == typeof(string))) && depth < 10)
                        {
                            securedObject = ProcessObjectFieldSecurity(securedValue, null, depth++);
                        }
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
            authorizationResponse.AuthorizationMessage = authorizationMessageStringBuilder.ToString();

            return authorizationResponse;
        }

        public string GetAccessTokenForLoggedInUser(string userId)
        {
            return _securityAccessTokenProvider.GetAccessToken(userId);
        }
    }
}
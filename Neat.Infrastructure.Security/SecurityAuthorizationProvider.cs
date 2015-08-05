using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
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
                var fieldLevelAuthorizationResponse = ProcessObjectFieldSecurity(securedObject, originalObject, action);
                authorizationResponse.SecuredObject = fieldLevelAuthorizationResponse.SecuredObject;
                authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, fieldLevelAuthorizationResponse.AuthorizationMessage);
            }

            return authorizationResponse;
        }

        private AuthorizationResponse ProcessObjectFieldSecurity(object securedObject, object originalObject, string action, int depth = 0)
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
                var propertyType = securedPropertyInfo.PropertyType;
                var originalPropertyInfo = originalProperties[i];
                var securedPropertyInfoCustomAttributes = securedPropertyInfo.CustomAttributes;
                var securedValue = securedPropertyInfo.GetValue(securedObject, new object[] { });
                var originalValue = originalPropertyInfo.GetValue(originalObject, new object[] { });
                CustomAttributeData secureAttribute;
                if (action != "Read")
                {
                    secureAttribute = securedPropertyInfoCustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SecureWritePropertyAttribute));
                }
                else
                {
                    secureAttribute = securedPropertyInfoCustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SecureReadPropertyAttribute));
                }
                if (secureAttribute != null)
                {
                    var securedPropertyName = secureAttribute.NamedArguments.FirstOrDefault(x => x.MemberName == "PropertyName").TypedValue.Value as string;
                    if (securedPropertyName == null)
                    {
                        securedPropertyName = string.Format("{0}.{1}", securedPropertyInfo.PropertyType.FullName, securedPropertyInfo.Name);
                    }
                    var role = _securityACLProvider.GetCurrentUserRoleForObject(securedObject);
                    
                    if (securedValue != originalValue && !_securityPermissionProvider.CanPerformActionOnProperty(role, action, securedPropertyName))
                    {
                        authorizationFailureMessages.Add(string.Format("Current User with Role {0} Does Not Have Permission to Perform Action {1} on Property {2}", role, action, securedPropertyName));
                        securedValue = originalValue;
                    }
                    securedPropertyInfo.SetValue(securedObject, securedValue);
                }
                if (!(propertyType.IsPrimitive || propertyType.IsValueType || (propertyType == typeof(string))) && depth < _securityContext.FieldLevelSecurityEvaulationDepth)
                {
                    var recursiveAuthorizationResponse = ProcessObjectFieldSecurity(securedValue, originalValue, action, depth++);
                    securedPropertyInfo.SetValue(securedObject, recursiveAuthorizationResponse.SecuredObject);
                    authorizationResponse.AuthorizationMessage = string.Format("{0} {1}", authorizationResponse.AuthorizationMessage, recursiveAuthorizationResponse.AuthorizationMessage);
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

        public string GetAccessTokenForLoggedInUser(string userId)
        {
            return _securityAccessTokenProvider.GetAccessToken(userId);
        }
    }
}
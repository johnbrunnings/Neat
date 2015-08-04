using System.Collections.Generic;
using System.Linq;
using Neat.Infrastructure.Security.Attribute;
using Neat.Infrastructure.Security.Model.Response;

namespace Neat.Infrastructure.Security
{
    public class SecurityAuthorizationProvider : ISecurityAuthorizationProvider
    {
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;
        private readonly ISecurityACLProvider _securityACLProvider;
        private readonly ISecurityPermissionProvider _securityPermissionProvider;

        public SecurityAuthorizationProvider(ISecurityAccessTokenProvider securityAccessTokenProvider, ISecurityACLProvider securityACLProvider, ISecurityPermissionProvider securityPermissionProvider)
        {
            _securityAccessTokenProvider = securityAccessTokenProvider;
            _securityACLProvider = securityACLProvider;
            _securityPermissionProvider = securityPermissionProvider;
        }

        public AuthorizationResponse GetAuthorizationForAccessToken(string accessToken)
        {
            var user = _securityAccessTokenProvider.GetUserFromAccessToken(accessToken);
            var authorizationResponse = new AuthorizationResponse();

            if (user != null)
            {
                authorizationResponse.UserId = user.Id;
                authorizationResponse.IsAuthorized = true;
            }
            
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Session Has Expired!");
            }

            return authorizationResponse;
        }

        public AuthorizationResponse CheckAuthorization(object securedObject, object originalObject, string action)
        {
            var role = _securityACLProvider.GetRoleForObject(securedObject);
            var actions = _securityPermissionProvider.GetActionsForRole(role);
            var authorizationResponse = new AuthorizationResponse();
            authorizationResponse.IsAuthorized = actions.Contains(action);
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Current User with Role {0} does not have permission to perform Action {1}!", role, action);
            }

            var securelyProcessedObject = ProcessObjectSecurity(securedObject, originalObject);
            authorizationResponse.SecuredObject = securelyProcessedObject;

            return authorizationResponse;
        }

        private object ProcessObjectSecurity(object securedObject, object originalObject, int depth = 0)
        {
            var authorizationFailureMessages = new List<string>();
            if (securedObject == null)
            {
                return authorizationFailureMessages;
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
                    if (securedPropertyInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SecureWritePropertyAttribute)) != null)
                    {
                        var securedValue = securedPropertyInfo.GetValue(securedObject, new object[] { });
                        var originalValue = originalPropertyInfo.GetValue(originalObject, new object[] { });
                        var securedPropertyName = string.Format("{0}.{1}", securedPropertyInfo.PropertyType.FullName, securedPropertyInfo.Name);
                        var role = _securityACLProvider.GetRoleForObject(securedObject);
                        var propertyType = securedPropertyInfo.PropertyType;
                        if (securedValue != originalValue && !_securityPermissionProvider.CanWriteToProperty(role, securedPropertyName))
                        {
                            authorizationFailureMessages.Add(string.Format("Current User with Role {0} does not have Permission to Write to Property {1}", role, securedPropertyName));
                            securedValue = originalValue;
                        }
                        securedProperties[i].SetValue(securedObject, securedValue);
                        if (!(propertyType.IsPrimitive || propertyType.IsValueType || (propertyType == typeof(string))) && depth < 10)
                        {
                            securedObject = ProcessObjectSecurity(securedValue, originalValue, depth++);
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < securedProperties.Length; i++)
                {
                    var securedPropertyInfo = securedProperties[i];
                    if (securedPropertyInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SecureReadPropertyAttribute)) != null)
                    {
                        var securedValue = securedPropertyInfo.GetValue(securedObject, new object[] { });
                        var securedPropertyName = string.Format("{0}.{1}", securedPropertyInfo.PropertyType.FullName, securedPropertyInfo.Name);
                        var role = _securityACLProvider.GetRoleForObject(securedObject);
                        var propertyType = securedPropertyInfo.PropertyType;
                        if (!_securityPermissionProvider.CanReadFromProperty(role, securedPropertyName))
                        {
                            authorizationFailureMessages.Add(string.Format("Current User with Role {0} does not have Permission to Read to Property {1}", role, securedPropertyName));
                            securedPropertyInfo.SetValue(securedObject, null, null);
                        }
                        if (!(propertyType.IsPrimitive || propertyType.IsValueType || (propertyType == typeof(string))) && depth < 10)
                        {
                            securedObject = ProcessObjectSecurity(securedValue, null, depth++);
                        }
                    }
                }
            }

            return securedObject;
        }

        public string GetAccessTokenForLoggedInUser(string userId)
        {
            return _securityAccessTokenProvider.GetAccessToken(userId);
        }
    }
}
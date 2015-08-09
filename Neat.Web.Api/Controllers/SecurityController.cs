using System.Web.Http;
using Neat.Infrastructure.Security;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.WebApi.Controllers;
using Neat.Infrastructure.WebApi.Filter;

namespace Neat.Web.Api.Controllers
{
    [ApiAuthorizationFilter]
    public class SecurityController : BaseApiController
    {
        private readonly ISecurityUserProvider _securityUserProvider;
        private readonly ISecurityAuthorizationProvider _securityAuthorizationProvider;
        private readonly ISecurityUserRoleProvider _securityUserRoleProvider;
        private readonly ISecurityUserPermissionProvider _securityUserPermissionProvider;
        private readonly ISecurityRoleActionProvider _securityRoleActionProvider;

        public SecurityController(ISecurityUserProvider securityUserProvider, ISecurityAuthorizationProvider securityAuthorizationProvider, ISecurityUserRoleProvider securityUserRoleProvider, ISecurityUserPermissionProvider securityUserPermissionProvider, ISecurityRoleActionProvider securityRoleActionProvider)
        {
            _securityUserProvider = securityUserProvider;
            _securityAuthorizationProvider = securityAuthorizationProvider;
            _securityUserRoleProvider = securityUserRoleProvider;
            _securityUserPermissionProvider = securityUserPermissionProvider;
            _securityRoleActionProvider = securityRoleActionProvider;
        }

        [HttpPost]
        public string VerifyAccessToken([FromBody] UserAuthenticationAccessTokenRequest userAuthenticationAccessTokenRequest)
        {
            if (_securityAuthorizationProvider.GetAuthorizationForAccessToken(userAuthenticationAccessTokenRequest.AccessToken).IsAuthorized)
            {
                return _securityAuthorizationProvider.GetAccessTokenForLoggedInUser(_securityUserProvider.GetCurrentUser().Id);
            }
            return null;
        }

        [HttpPost]
        public string CreateUser([FromBody] User user)
        {
            return _securityUserProvider.CreateUser(user);
        }

        [HttpPost]
        public void AssignRoleToUser([FromBody] UserRoleRequest userRoleRequest)
        {
            _securityUserRoleProvider.CreateUserRole(userRoleRequest.Role);
        }

        [HttpPost]
        public void AssignRoleToUserForObject([FromBody] UserRoleForObjectRequest userRoleForObjectRequest)
        {
            _securityUserRoleProvider.CreateUserRoleForObject(userRoleForObjectRequest.Role, userRoleForObjectRequest.ObjectType, userRoleForObjectRequest.ObjectId);
        }

        [HttpDelete]
        public void DeleteRoleFromUser()
        {
            _securityUserRoleProvider.DeleteUserRole();
        }

        [HttpDelete]
        public void DeleteRoleFromUserForObject(string objectType, string objectId)
        {
            _securityUserRoleProvider.DeleteUserRoleForObject(objectType, objectId);
        }

        [HttpPost]
        public void AddUserPermission([FromBody] UserPermissionRequest userPermissionRequest)
        {
            _securityUserPermissionProvider.CreateUserPermission(userPermissionRequest.Role, userPermissionRequest.Action, userPermissionRequest.PropertyName);
        }

        [HttpDelete]
        public void DeleteUserPermission(string role, string action, string propertyName)
        {
            _securityUserPermissionProvider.RemoveUserPermission(role, action, propertyName);
        }

        [HttpPost]
        public void AddRoleAction([FromBody] RoleActionRequest roleActionRequest)
        {
            _securityRoleActionProvider.CreateRoleAction(roleActionRequest.Role, roleActionRequest.Action);
        }

        [HttpDelete]
        public void DeleteRoleAction(string role, string action)
        {
            _securityRoleActionProvider.RemoveRoleAction(role, action);
        }
    }
}
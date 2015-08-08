using System.ComponentModel;
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

        public SecurityController(ISecurityUserProvider securityUserProvider, ISecurityAuthorizationProvider securityAuthorizationProvider, ISecurityUserRoleProvider securityUserRoleProvider)
        {
            _securityUserProvider = securityUserProvider;
            _securityAuthorizationProvider = securityAuthorizationProvider;
            _securityUserRoleProvider = securityUserRoleProvider;
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
    }
}
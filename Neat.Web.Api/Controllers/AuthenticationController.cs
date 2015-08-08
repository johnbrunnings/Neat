using System.Web.Http;
using Neat.Infrastructure.Security;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.WebApi.Controllers;

namespace Neat.Web.Api.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly ISecurityAuthenticationProvider _securityAuthenticationProvider;

        public AuthenticationController(ISecurityAuthenticationProvider securityAuthenticationProvider, ISecurityUserProvider securityUserProvider, ISecurityAuthorizationProvider securityAuthorizationProvider, ISecurityUserRoleProvider securityUserRoleProvider)
        {
            _securityAuthenticationProvider = securityAuthenticationProvider;
        }

        [HttpPost]
        public string Authenticate([FromBody] UserAuthenticationRequest userAuthenticationRequest)
        {
            return _securityAuthenticationProvider.Authenticate(userAuthenticationRequest);
        }

        [HttpPost]
        public void Logout([FromBody] UserAuthenticationAccessTokenRequest userAuthenticationAccessTokenRequest)
        {
            _securityAuthenticationProvider.Logout(userAuthenticationAccessTokenRequest.AccessToken);
        }
    }
}
using System.Web.Http;
using Neat.Infrastructure.Security;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.WebApi.Controllers;

namespace Neat.Web.Api.Controllers
{
    public class SecurityController : BaseApiController
    {
        private readonly ISecurityAuthenticationProvider _securityAuthenticationProvider;
        private readonly ISecurityUserProvider _securityUserProvider;
        private readonly ISecurityAuthorizationProvider _securityAuthorizationProvider;

        public SecurityController(ISecurityAuthenticationProvider securityAuthenticationProvider, ISecurityUserProvider securityUserProvider, ISecurityAuthorizationProvider securityAuthorizationProvider)
        {
            _securityAuthenticationProvider = securityAuthenticationProvider;
            _securityUserProvider = securityUserProvider;
            _securityAuthorizationProvider = securityAuthorizationProvider;
        }

        [HttpPost]
        public string Authenticate([FromBody] UserAuthenticationRequest userAuthenticationRequest)
        {
            return _securityAuthenticationProvider.Authenticate(userAuthenticationRequest);
        }

        [HttpPost]
        public bool VerifyAccessToken([FromBody] UserAuthenticationAccessTokenRequest userAuthenticationAccessTokenRequest)
        {
            return _securityAuthorizationProvider.GetAuthorizationForAccessToken(userAuthenticationAccessTokenRequest.AccessToken).IsAuthorized;
        }

        [HttpPost]
        public void Logout([FromBody] UserAuthenticationAccessTokenRequest userAuthenticationAccessTokenRequest)
        {
            _securityAuthenticationProvider.Logout(userAuthenticationAccessTokenRequest.AccessToken);
        }

        [HttpPost]
        public string CreateUser([FromBody] User user)
        {
            return _securityUserProvider.CreateUser(user);
        }
    }
}
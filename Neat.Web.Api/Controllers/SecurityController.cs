using System.Web.Http;
using Neat.Infrastructure.Security;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.WebApi.Controllers;

namespace Neat.Web.Api.Controllers
{
    public class SecurityController : BaseApiController
    {
        private readonly ISecurityAuthenticationProvider _securityAuthenticationProvider;

        public SecurityController(ISecurityAuthenticationProvider securityAuthenticationProvider)
        {
            _securityAuthenticationProvider = securityAuthenticationProvider;
        }

        [HttpPost]
        public string Authenticate(UserAuthenticationRequest userAuthenticationRequest)
        {
            return _securityAuthenticationProvider.Authenticate(userAuthenticationRequest);
        }

        [HttpPost]
        public void Logout(string userAccessToken)
        {
            _securityAuthenticationProvider.Logout(userAccessToken);
        }
    }
}
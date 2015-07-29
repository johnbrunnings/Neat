using Neat.Infrastructure.Security.Model.Request;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityAuthenticationProvider
    {
        string Authenticate(UserAuthenticationRequest userAuthenticationRequest);
        void Logout(string userAccessToken);
    }
}
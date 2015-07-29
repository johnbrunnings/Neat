using Neat.Infrastructure.Security.Model.Response;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityAuthorizationProvider
    {
        AuthorizationResponse GetAuthorizationForAccessToken(string accessToken);
    }
}
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityAccessTokenProvider
    {
        string CreateAccessToken(string userId, UserAuthenticationRequest userAuthenticationRequest);
        string GetAccessToken(string userId);
        User GetUserFromAccessToken(string accessToken);
    }
}
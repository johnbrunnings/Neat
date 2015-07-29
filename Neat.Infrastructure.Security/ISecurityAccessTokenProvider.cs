using Neat.Infrastructure.Security.Model;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityAccessTokenProvider
    {
        string GetAccessToken(Session session);
        Session GetSessionFromAccessToken(string accessToken);
    }
}
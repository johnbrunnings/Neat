using Neat.Infrastructure.Security.Model.Response;

namespace Neat.Infrastructure.Security
{
    public interface ISecurityAuthorizationProvider
    {
        AuthorizationResponse GetAuthorizationForAccessToken(string accessToken);
        AuthorizationResponse CheckUserAuthorization(string action);
        AuthorizationResponse CheckObjectAuthorization(object securedObject, object originalObject, string action);
        string GetAccessTokenForLoggedInUser(string userId);
    }
}
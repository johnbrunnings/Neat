using Neat.Infrastructure.Security.Model.Response;

namespace Neat.Infrastructure.Security
{
    public class SecurityAuthorizationProvider : ISecurityAuthorizationProvider
    {
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;

        public SecurityAuthorizationProvider(ISecurityAccessTokenProvider securityAccessTokenProvider)
        {
            _securityAccessTokenProvider = securityAccessTokenProvider;
        }

        public AuthorizationResponse GetAuthorizationForAccessToken(string accessToken)
        {
            var user = _securityAccessTokenProvider.GetUserFromAccessToken(accessToken);
            var authorizationResponse = new AuthorizationResponse();

            if (user != null)
            {
                authorizationResponse.UserId = user.Id;
                authorizationResponse.IsAuthorized = true;
            }
            
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Session Has Expired!");
            }

            return authorizationResponse;
        }

        public string GetAccessTokenForLoggedInUser(string userId)
        {
            return _securityAccessTokenProvider.GetAccessToken(userId);
        }
    }
}
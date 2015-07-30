using System;
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
            var session = _securityAccessTokenProvider.GetSessionFromAccessToken(accessToken);
            var authorizationResponse = new AuthorizationResponse()
            {
                UserId = session.UserId,
                IsAuthorized = DateTime.UtcNow <= session.ExpirationDate
            };
            if (!authorizationResponse.IsAuthorized)
            {
                authorizationResponse.AuthorizationMessage = string.Format("Session has expired!");
            }

            return authorizationResponse;
        }
    }
}
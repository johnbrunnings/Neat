using System;
using System.Linq;
using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityAuthenticationProvider : ISecurityAuthenticationProvider
    {
        private readonly IStorageApplication<User> _userStorageApplication;
        private readonly IHashProvider _hashProvider;
        private readonly ISecurityStorageProvider _securityStorageProvider;
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;

        public SecurityAuthenticationProvider(IStorageApplication<User> userStorageApplication, IHashProvider hashProvider, ISecurityStorageProvider securityStorageProvider, ISecurityAccessTokenProvider securityAccessTokenProvider)
        {
            _userStorageApplication = userStorageApplication;
            _hashProvider = hashProvider;
            _securityStorageProvider = securityStorageProvider;
            _securityAccessTokenProvider = securityAccessTokenProvider;
        }

        public string Authenticate(UserAuthenticationRequest userAuthenticationRequest)
        {
            var encryptedPassword = _hashProvider.Hash(userAuthenticationRequest.Password);
            var user = _userStorageApplication.GetAll()
                .Where(
                    u =>
                        u.Username.ToLower() == userAuthenticationRequest.Username &&
                        u.Password == encryptedPassword)
                .FirstOrDefault();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var session = new Session()
            {
                UserId = user.Id,
                StartDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow
                                            .AddDays(userAuthenticationRequest.Duration.Days)
                                            .AddHours(userAuthenticationRequest.Duration.Hours)
                                            .AddMinutes(userAuthenticationRequest.Duration.Minutes)
                                            .AddSeconds(userAuthenticationRequest.Duration.Seconds)
            };

            _securityStorageProvider.CreateSession(session);

            var accessToken = _securityAccessTokenProvider.GetAccessToken(session);

            return accessToken;
        }

        public void Logout(string userAccessToken)
        {
            var session = _securityAccessTokenProvider.GetSessionFromAccessToken(userAccessToken);
            session.ExpirationDate = DateTime.UtcNow;
            _securityStorageProvider.UpdateSession(session);
        }
    }
}
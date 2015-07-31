using System;
using System.Linq;
using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.Security.Storage;
using Neat.Infrastructure.Session;

namespace Neat.Infrastructure.Security
{
    public class SecurityAuthenticationProvider : ISecurityAuthenticationProvider
    {
        private readonly IStorageApplication<User> _userStorageApplication;
        private readonly IHashProvider _hashProvider;
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;
        private readonly ISessionProvider _sessionProvider;

        public SecurityAuthenticationProvider(IStorageApplication<User> userStorageApplication, IHashProvider hashProvider, ISecurityAccessTokenProvider securityAccessTokenProvider, ISessionProvider sessionProvider)
        {
            _userStorageApplication = userStorageApplication;
            _hashProvider = hashProvider;
            _securityAccessTokenProvider = securityAccessTokenProvider;
            _sessionProvider = sessionProvider;
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

            var accessToken = _securityAccessTokenProvider.CreateAccessToken(user.Id, userAuthenticationRequest);

            return accessToken;
        }

        public void Logout(string userAccessToken)
        {
            var user = _securityAccessTokenProvider.GetUserFromAccessToken(userAccessToken);
            if (user != null)
            {
                _sessionProvider.Logout(user.Id);
            }
        }
    }
}
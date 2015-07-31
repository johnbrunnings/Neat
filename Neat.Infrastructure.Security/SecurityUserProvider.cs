using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Security.Context;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public class SecurityUserProvider : ISecurityUserProvider
    {
        private readonly ISecurityStorageProvider _securityStorageProvider;
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly ISecurityContext _securityContext;

        public SecurityUserProvider(ISecurityStorageProvider securityStorageProvider, ISecurityAccessTokenProvider securityAccessTokenProvider, IEncryptionProvider encryptionProvider, ISecurityContext securityContext)
        {
            _securityStorageProvider = securityStorageProvider;
            _securityAccessTokenProvider = securityAccessTokenProvider;
            _encryptionProvider = encryptionProvider;
            _securityContext = securityContext;
        }

        public string CreateUser(User user)
        {
            user.Password = _encryptionProvider.Encrypt(user.Password);
            var newUser = _securityStorageProvider.Add(user);
            if (!_securityContext.EnableLoginUserOnCreation)
            {
                return null;
            }

            var loginUserOnCreationDuration = _securityContext.LoginUserOnCreationDuration;
            var userAuthenticationRequest = new UserAuthenticationRequest()
            {
                Duration = new UserAuthorizationDurationRequest()
                {
                    Days = loginUserOnCreationDuration.Days,
                    Hours = loginUserOnCreationDuration.Hours,
                    Minutes = loginUserOnCreationDuration.Minutes,
                    Seconds = loginUserOnCreationDuration.Seconds
                }
            };

            return _securityAccessTokenProvider.CreateAccessToken(newUser.Id, userAuthenticationRequest);
        }

        public void UpdateUser(User user)
        {
            _securityStorageProvider.Update(user);
        }

        public User GetUser(string userId)
        {
            return _securityStorageProvider.GetById(userId);
        }
    }
}
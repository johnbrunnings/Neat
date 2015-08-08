using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Security.Context;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.Security.Storage;
using Neat.Infrastructure.Session;

namespace Neat.Infrastructure.Security
{
    public class SecurityUserProvider : ISecurityUserProvider
    {
        private readonly IUserSecurityStorageProvider _userSecurityStorageProvider;
        private readonly ISecurityAccessTokenProvider _securityAccessTokenProvider;
        private readonly IHashProvider _hashProvider;
        private readonly ISecurityContext _securityContext;
        private readonly ISessionProvider _sessionProvider;

        public SecurityUserProvider(IUserSecurityStorageProvider userSecurityStorageProvider, ISecurityAccessTokenProvider securityAccessTokenProvider, IHashProvider hashProvider, ISecurityContext securityContext, ISessionProvider sessionProvider)
        {
            _userSecurityStorageProvider = userSecurityStorageProvider;
            _securityAccessTokenProvider = securityAccessTokenProvider;
            _hashProvider = hashProvider;
            _securityContext = securityContext;
            _sessionProvider = sessionProvider;
        }

        public string CreateUser(User user)
        {
            user.Password = _hashProvider.Hash(user.Password);
            user.Username = user.Username.ToLower();
            var newUser = _userSecurityStorageProvider.Add(user);
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
            _userSecurityStorageProvider.Update(user);
        }

        public User GetUser(string userId)
        {
            return _userSecurityStorageProvider.GetById(userId);
        }

        public User GetCurrentUser()
        {
            var session = _sessionProvider.GetCurrentSession();
            if (session == null)
            {
                return null;
            }

            return _userSecurityStorageProvider.GetById(session.UserId);
        }
    }
}
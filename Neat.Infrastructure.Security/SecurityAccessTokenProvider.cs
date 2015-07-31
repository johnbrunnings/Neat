using System.Security.Authentication;
using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Security.Model;
using Neat.Infrastructure.Security.Model.Request;
using Neat.Infrastructure.Security.Storage;
using Neat.Infrastructure.Session;
using Neat.Infrastructure.Session.Model.Request;
using Newtonsoft.Json;

namespace Neat.Infrastructure.Security
{
    public class SecurityAccessTokenProvider : ISecurityAccessTokenProvider
    {
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly ISessionProvider _sessionProvider;
        private readonly ISecurityStorageProvider _securityStorageProvider;

        public SecurityAccessTokenProvider(IEncryptionProvider encryptionProvider, ISessionProvider sessionProvider, ISecurityStorageProvider securityStorageProvider)
        {
            _encryptionProvider = encryptionProvider;
            _sessionProvider = sessionProvider;
            _securityStorageProvider = securityStorageProvider;
        }

        public string CreateAccessToken(string userId, UserAuthenticationRequest userAuthenticationRequest)
        {
            var sessionDurationRequest = new SessionDurationRequest()
            {
                Days = userAuthenticationRequest.Duration.Days,
                Hours = userAuthenticationRequest.Duration.Hours,
                Minutes = userAuthenticationRequest.Duration.Minutes,
                Seconds = userAuthenticationRequest.Duration.Seconds
            };

            var session = _sessionProvider.CreateSession(userId, sessionDurationRequest);

            return _encryptionProvider.Encrypt(JsonConvert.SerializeObject(session));
        }

        public string GetAccessToken(string userId)
        {
            var session = _sessionProvider.GetSession(userId);

            if (session == null)
            {
                throw new AuthenticationException("Session Has Expired!");
            }

            return _encryptionProvider.Encrypt(JsonConvert.SerializeObject(session));
        }

        public User GetUserFromAccessToken(string accessToken)
        {
            var session = JsonConvert.DeserializeObject<Session.Model.Session>(_encryptionProvider.Decrypt(accessToken));
            session = _sessionProvider.ValidateSession(session);
            if (session == null)
            {
                return null;
            }
            var user = _securityStorageProvider.GetById(session.UserId);

            return user;
        }
    }
}
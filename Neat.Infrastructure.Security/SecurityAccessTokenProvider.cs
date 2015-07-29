using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Security.Model;
using Newtonsoft.Json;

namespace Neat.Infrastructure.Security
{
    public class SecurityAccessTokenProvider : ISecurityAccessTokenProvider
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public SecurityAccessTokenProvider(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public string GetAccessToken(Session session)
        {
            return _encryptionProvider.Encrypt(JsonConvert.SerializeObject(session));
        }

        public Session GetSessionFromAccessToken(string accessToken)
        {
            return JsonConvert.DeserializeObject<Session>(_encryptionProvider.Decrypt(accessToken));
        }
    }
}
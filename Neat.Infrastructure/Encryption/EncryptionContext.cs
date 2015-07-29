using Neat.Infrastructure.Config;

namespace Neat.Infrastructure.Encryption
{
    public class EncryptionContext : IEncryptionContext
    {
        private readonly IConfig _config;

        public EncryptionContext(IConfig config)
        {
            _config = config;
        }

        public string Key { get { return _config.GetSetting("Encryption:Key"); } }
        public string Salt { get { return _config.GetSetting("Encryption:Salt"); } }
    }
}
using System.Security.Cryptography;
using Neat.Encryption.ProviderFactory.Interface;

namespace Neat.Encryption.ProviderFactory
{
    public class AesCryptoServiceProviderFactory : IAesCryptoServiceProviderFactory
    {
        public Aes Create()
        {
            return new AesCryptoServiceProvider();
        }
    }
}
using System.Security.Cryptography;

namespace Neat.Encryption.ProviderFactory.Interface
{
    public interface IAesCryptoServiceProviderFactory
    {
        Aes Create();
    }
}
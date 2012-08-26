using Neat.Encryption.Parameters.Abstract;

namespace Neat.Encryption.Strategy.Interface
{
    public interface IEncryptionStrategy
    {
        EncryptionMethod EncryptionMethod { get; }
        byte[] Encrypt(EncryptionParameters encryptionParameters);
        byte[] Decrypt(EncryptionParameters encryptionParameters);
    }
}
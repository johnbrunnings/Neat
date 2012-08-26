namespace Neat.Encryption.Strategy.Interface
{
    public interface IEncryptionStrategy
    {
        EncryptionMethod EncryptionMethod { get; }
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
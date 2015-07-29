namespace Neat.Infrastructure.Encryption
{
    public interface IEncryptionProvider
    {
        string Encrypt(string value);
        string Decrypt(string encryptedValue);
    }
}
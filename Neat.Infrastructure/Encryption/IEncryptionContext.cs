namespace Neat.Infrastructure.Encryption
{
    public interface IEncryptionContext
    {
        string Key { get; }
        string Salt { get; }
    }
}
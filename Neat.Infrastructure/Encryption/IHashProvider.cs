namespace Neat.Infrastructure.Encryption
{
    public interface IHashProvider
    {
        string Hash(string value);
    }
}
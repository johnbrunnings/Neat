namespace Neat.Data
{
    public interface IConnectionFactory
    {
        string GetConnectionString<T>();
    }
}
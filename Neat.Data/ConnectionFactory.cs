using Neat.Infrastructure.Config;

namespace Neat.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfig _config;

        public ConnectionFactory(IConfig config)
        {
            _config = config;
        }

        public string GetConnectionString<T>()
        {
            return _config.GetConnectionString("MongoServerSettings");
        }
    }
}
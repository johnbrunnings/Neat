using Neat.Infrastructure.Config;

namespace Neat.Infrastructure.WebApi.Context
{
    public class CORSContext : ICORSContext
    {
        private readonly IConfig _config;

        public CORSContext(IConfig config)
        {
            _config = config;
        }

        public string Domains { get { return _config.GetSetting("CORS:Domains"); } }
    }
}
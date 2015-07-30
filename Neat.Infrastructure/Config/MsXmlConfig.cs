using System;
using System.Configuration;

namespace Neat.Infrastructure.Config
{
    public class MsXmlConfig : IConfig
    {
        public string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public string GetContainer()
        {
            throw new NotImplementedException();
        }
    }
}
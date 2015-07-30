using System.Web.Http;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json.Serialization;

namespace Neat.Infrastructure.WebApi
{
    public class SerializationConfig
    {
        public static void Setup()
        {
            // MongoDB
            ConventionRegistry.Register("Neat.Web.Api Conventions", new ConventionPack { new CamelCaseElementNameConvention() }, type => true);

            // Newtonsoft.Json
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver() { IgnoreSerializableInterface = true };
        }
    }
}
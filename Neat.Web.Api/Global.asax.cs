using System.Web.Http;
using Neat.Infrastructure.WebApi;

namespace Neat.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            SerializationConfig.Setup();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
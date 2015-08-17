using Microsoft.Practices.Unity;
using Neat.Infrastructure.Unity;

namespace Neat.Web.Api
{
    public static class Bootstrapper
    {
        public static void Register()
        {
            var container = new LoggingUnityContainer(new UnityContainer());

            Register(container);
        }

        private static void Register(IUnityContainer container)
        {
            Neat.Infrastructure.WebApi.Bootstrapper.Attach(container);
            
        }
    }
}
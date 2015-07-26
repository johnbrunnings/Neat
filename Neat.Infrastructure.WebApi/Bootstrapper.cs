using System.Web.Http;
using Microsoft.Practices.Unity;
using Neat.Infrastructure.Unity;
using Neat.Infrastructure.WebApi.Context;
using Unity.WebApi;

namespace Neat.Infrastructure.WebApi
{
    public static class Bootstrapper
    {
        public static void Register()
        {
            var container = new LoggingUnityContainer(new UnityContainer());

            Register(container);
        }

        public static void Attach(IUnityContainer container)
        {
            Register(container);
        }

        private static void Register(IUnityContainer container)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            Neat.Infrastructure.Bootstrapper.Attach(container);
            Neat.Service.Bootstrapper.Attach(container);

            container.RegisterType<ICORSContext, CORSContext>(new ContainerControlledLifetimeManager());
        }
    }
}
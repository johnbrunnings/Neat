using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using Neat.Infrastructure.Unity;
using Neat.Infrastructure.WebApi.Context;
using Neat.Infrastructure.WebApi.Filter;
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

            // Register Filter Injection for Web Api
            var providers = GlobalConfiguration.Configuration.Services.GetFilterProviders().ToList();

            GlobalConfiguration.Configuration.Services.Add(
                typeof(IFilterProvider),
                new UnityWebApiFilterProvider(container));

            var defaultprovider = providers.First(p => p is ActionDescriptorFilterProvider);

            GlobalConfiguration.Configuration.Services.Remove(
                typeof(IFilterProvider),
                defaultprovider);

            Neat.Infrastructure.Bootstrapper.Attach(container);
            Neat.Service.Bootstrapper.Attach(container);

            container.RegisterType<ICORSContext, CORSContext>(new ContainerControlledLifetimeManager());
        }
    }
}
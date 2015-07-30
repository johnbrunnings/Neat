using Microsoft.Practices.Unity;
using Neat.Infrastructure.Security.Storage;

namespace Neat.Infrastructure.Security
{
    public static class Bootstrapper
    {
        public static void Register()
        {
            var container = new UnityContainer();

            Register(container);
        }

        public static void Attach(IUnityContainer container)
        {
            Register(container);
        }

        private static void Register(IUnityContainer container)
        {
            Neat.Data.Mongo.Bootstrapper.Attach(container);
            Neat.Infrastructure.Bootstrapper.Attach(container);

            container.RegisterType(typeof(IStorageApplication<>), typeof(MongoStorageApplication<>),
                new ContainerControlledLifetimeManager());

            container.RegisterType<ISecurityAccessTokenProvider, SecurityAccessTokenProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityAuthorizationProvider, SecurityAuthorizationProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityAuthenticationProvider, SecurityAuthenticationProvider>(new ContainerControlledLifetimeManager());
        }
    }
}
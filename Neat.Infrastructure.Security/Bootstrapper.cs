using Microsoft.Practices.Unity;
using Neat.Infrastructure.ApplicationProcessing;
using Neat.Infrastructure.Security.ApplicationProcessing;
using Neat.Infrastructure.Security.Context;
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

            container.RegisterType<IUserSecurityStorageProvider, UserSecurityStorageProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserRoleSecurityStorageProvider, UserRoleSecurityStorageProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityAccessTokenProvider, SecurityAccessTokenProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityAuthorizationProvider, SecurityAuthorizationProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityAuthenticationProvider, SecurityAuthenticationProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityUserProvider, SecurityUserProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityUserRoleProvider, SecurityUserRoleProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityContext, SecurityContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApplicationProcessingRule, SecurityApplicationProcessingRule>("SecurityApplicationProcessingRule", new ContainerControlledLifetimeManager());
        }
    }
}
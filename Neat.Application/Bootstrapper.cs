using Microsoft.Practices.Unity;
using Neat.Infrastructure.Security;
using Neat.Infrastructure.Unity;
using Neat.Infrastructure.Unity.Interceptor;

namespace Neat.Application
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
            Neat.Data.Bootstrapper.Attach(container);
            Neat.Data.Mongo.Bootstrapper.Attach(container);
            Neat.Infrastructure.Bootstrapper.Attach(container);
            Neat.Infrastructure.Session.Bootstrapper.Attach(container);
            Neat.Infrastructure.Security.Bootstrapper.Attach(container);
            Neat.Infrastructure.Validation.Bootstrapper.Attach(container);

            container.RegisterType<ISecurityACLProvider, SecurityACLProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityPermissionProvider, SecurityPermissionProvider>(new ContainerControlledLifetimeManager());

            container.RegisterApplicationWithInterceptor(typeof(IDomainApplication<>), typeof(DomainApplication<>), new[] { typeof(ApplicationProcessingInterceptor) });

            container.RegisterAllServiceWithInterceptors<IApplication>(new[] { typeof(ApplicationProcessingInterceptor) });
        }
    }
}
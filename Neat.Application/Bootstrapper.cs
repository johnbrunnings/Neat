using Microsoft.Practices.Unity;
using Neat.Infrastructure.Unity;

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
            Neat.Data.Mongo.Bootstrapper.Attach(container);
            Neat.Infrastructure.Bootstrapper.Attach(container);

            container.RegisterType(typeof (IDomainApplication<>), typeof (DomainApplication<>),
                new ContainerControlledLifetimeManager());

            container.RegisterAllService<IApplication>();
        }
    }
}
using Microsoft.Practices.Unity;
using Neat.Infrastructure.Unity;

namespace Neat.Service
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
            Neat.Application.Bootstrapper.Attach(container);

            container.RegisterType(typeof (IDomainService<>), typeof (DomainService<>),
                new ContainerControlledLifetimeManager());

            container.RegisterAllService<IService>();
        }
    }
}
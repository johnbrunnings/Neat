using Microsoft.Practices.Unity;
using Neat.Infrastructure.ApplicationProcessing;
using Neat.Infrastructure.Validation.ApplicationProcessing;
using Neat.Infrastructure.Validation.Context;

namespace Neat.Infrastructure.Validation
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
            Neat.Infrastructure.Bootstrapper.Attach(container);

            container.RegisterType<IValidationContext, ValidationContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApplicationProcessingRule, ValidationApplicationProcessingRule>("ValidationApplicationProcessingRule", new ContainerControlledLifetimeManager());
        }
    }
}
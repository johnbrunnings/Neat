using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Neat.Infrastructure.ApplicationProcessing;
using Neat.Infrastructure.Config;
using Neat.Infrastructure.Encryption;
using Neat.Infrastructure.Logging;
using Neat.Infrastructure.Unity;

namespace Neat.Infrastructure
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
            container.RegisterType<IConfig, MsXmlConfig>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApplicationProcessor, ApplicationProcessor>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEncryptionProvider, AesEncryptionProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHashProvider, Sha512HashProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEncryptionContext, EncryptionContext>(new ContainerControlledLifetimeManager());

            // Bypass normal LoggingContainer registration to prevent Stack Overflow
            ((LoggingUnityContainer)container).RegisterTypeWithoutLogging(typeof(ILogProvider), typeof(NLogProvider), null, new ContainerControlledLifetimeManager());

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
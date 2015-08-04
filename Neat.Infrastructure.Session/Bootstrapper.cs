using Microsoft.Practices.Unity;
using Neat.Infrastructure.Session.Context;
using Neat.Infrastructure.Session.Storage;

namespace Neat.Infrastructure.Session
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

            container.RegisterType<ISessionStorageProvider, SessionStorageProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISessionGraceStorageProvider, SessionGraceStorageProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISessionProvider, SessionProvider>("SessionProvider", new ContainerControlledLifetimeManager());
            container.RegisterType<ISessionProvider, SessionRecyclingSessionProviderDecorator>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<ISessionProvider>("SessionProvider"),
                    new ResolvedParameter<ISessionContext>(),
                    new ResolvedParameter<ISessionGraceStorageProvider>()));
            container.RegisterType<ISessionContext, SessionContext>(new ContainerControlledLifetimeManager());
        }
    }
}
using Microsoft.Practices.Unity;

namespace Neat.Data.Mongo
{
    public static class Bootstrapper
    {
        public static IUnityContainer Register()
        {
            var container = new UnityContainer();

            Register(container);

            return container;
        }

        public static void Attach(IUnityContainer container)
        {
            Register(container);
        }

        private static void Register(IUnityContainer container)
        {
            container.RegisterType(typeof(IRepository<>), typeof(MongoRepository<>),
                new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository, GenericMongoRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
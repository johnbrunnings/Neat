using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Neat.Infrastructure.Unity
{
    public static class UnityExtensions
    {
        public static IUnityContainer RegisterAllForInterface<TIService>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            return container.RegisterAllForInterface(typeof (TIService), injectionMembers);
        }

        public static IUnityContainer RegisterAllForInterface(this IUnityContainer container, Type interfaceType, params InjectionMember[] injectionMembers)
        {
            return RegisterAllForInterface(container, interfaceType, WithName.TypeName, injectionMembers);
        }

        public static IUnityContainer RegisterAllService<TIService>(this IUnityContainer container)
        {
            Type interfaceType = typeof(TIService);

            foreach (var type in AllClasses.FromLoadedAssemblies().Where(type => type.GetInterfaces().Contains(interfaceType)))
            {
                foreach (var i in type.GetInterfaces().Where(x=>x != interfaceType))
                {
                    container.RegisterType(i, type, new ContainerControlledLifetimeManager());
                }
            }
            return container;
        }

        public static IUnityContainer RegisterAllForInterface(this IUnityContainer container, Type interfaceType,
            Func<Type, string> withTypeName, params InjectionMember[] injectionMembers)
        {
            foreach (var type in AllClasses.FromLoadedAssemblies().Where(type => type.GetInterfaces().Contains(interfaceType)))
            {
                container.RegisterType(interfaceType, type, withTypeName(type), new ContainerControlledLifetimeManager(), injectionMembers);
            }
            return container;
        }

        public static IUnityContainer RegisterStrategyExecutor<TIStrategyExecutor, TStrategyExecutor, TIStrategy>(
            this IUnityContainer container) where TStrategyExecutor : TIStrategyExecutor
        {
            container.RegisterAllForInterface(typeof (TIStrategy));

            return container.RegisterType<TIStrategyExecutor, TStrategyExecutor>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<TIStrategy[]>()));
        }

        public static IUnityContainer RegisterService<TIService, TService>(this IUnityContainer container)
            where TService : TIService
        {
            container.RegisterType<TIService, TService>(new ContainerControlledLifetimeManager());
            return container;
        }
    }
}
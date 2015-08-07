using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

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

        public static IUnityContainer RegisterAllServiceWithInterceptors<TIService>(this IUnityContainer container, Type[] interceptorTypes)
        {
            Type interfaceType = typeof(TIService);

            foreach (var type in AllClasses.FromLoadedAssemblies().Where(type => type.GetInterfaces().Contains(interfaceType)))
            {
                foreach (var i in type.GetInterfaces().Where(x => x != interfaceType))
                {
                    container.RegisterApplicationWithInterceptor(i, type, interceptorTypes);
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

        public static void RegisterApplicationWithInterceptor(this IUnityContainer container, Type i, Type iType, Type[] interceptorTypes, string name = null, InjectionConstructor injectionConstructor = null)
        {
            var interceptingInjectionMembers = new List<InjectionMember>()
            {
                new Interceptor<InterfaceInterceptor>(),
            };
            foreach (var type in interceptorTypes)
            {
                interceptingInjectionMembers.Add(new InterceptionBehavior(type));
            }
            if (injectionConstructor != null)
            {
                interceptingInjectionMembers.Add(injectionConstructor);
            }

            if (string.IsNullOrEmpty(name) && injectionConstructor == null)
            {
                container.RegisterType(i, iType, new ContainerControlledLifetimeManager(), interceptingInjectionMembers.ToArray());
            }
            else
            {
                container.RegisterType(i, iType, name, new ContainerControlledLifetimeManager(), interceptingInjectionMembers.ToArray());
            }
        }

        public static void RegisterApplicationWithInterceptor<TInterface, TImpl>(this IUnityContainer container, Type[] interceptorTypes, string name = null, InjectionConstructor injectionConstructor = null)
            where TInterface : class
            where TImpl : TInterface
        {
            var interceptingInjectionMembers = new List<InjectionMember>()
            {
                new Interceptor<InterfaceInterceptor>(),
            };
            foreach (var type in interceptorTypes)
            {
                interceptingInjectionMembers.Add(new InterceptionBehavior(type));
            }
            if (injectionConstructor != null)
            {
                interceptingInjectionMembers.Add(injectionConstructor);
            }

            if (string.IsNullOrEmpty(name) && injectionConstructor == null)
            {
                container.RegisterType<TInterface, TImpl>(new ContainerControlledLifetimeManager(), interceptingInjectionMembers.ToArray());
            }
            else
            {
                container.RegisterType<TInterface, TImpl>(name, new ContainerControlledLifetimeManager(), interceptingInjectionMembers.ToArray());
            }
        }

        public static IUnityContainer RegisterTypeDirectlyAgainstContainer(this IUnityContainer container, Type @from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            var additionalInjectionMembers = new InjectionMember[injectionMembers.Length + 1];
            additionalInjectionMembers[injectionMembers.Length] = new BypassLoggingInjectionMember();
            return container.RegisterType(@from, to, name, lifetimeManager, additionalInjectionMembers);
        }
    }
}
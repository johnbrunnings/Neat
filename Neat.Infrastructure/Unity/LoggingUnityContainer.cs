using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Neat.Infrastructure.Unity.Interceptor;

namespace Neat.Infrastructure.Unity
{
    public class LoggingUnityContainer : IUnityContainer
    {
        private readonly IUnityContainer _unityContainer;

        public LoggingUnityContainer(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            _unityContainer.AddNewExtension<Interception>();
        }

        public void Dispose()
        {
            _unityContainer.Dispose();
        }

        public IUnityContainer RegisterType(Type @from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            var bypassLogging = injectionMembers.Any(injectionMember => injectionMember.GetType() == typeof (BypassLoggingInjectionMember));
            if (@from == null || bypassLogging)
            {
                // we're assuming this is just a component registration in which case our logging injection, by interface, will not work.
                return _unityContainer.RegisterType(@from, to, name, lifetimeManager, injectionMembers);
            }
            return _unityContainer.RegisterType(@from, to, name, lifetimeManager, GetInjectionMembersAndLogging(injectionMembers));
        }

        public IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            return _unityContainer.RegisterInstance(t, name, instance, lifetime);
        }

        public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
        {
            return _unityContainer.Resolve(t, name, resolverOverrides);
        }   

        public IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides)
        {
            return _unityContainer.ResolveAll(t, resolverOverrides);
        }

        public object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides)
        {
            return _unityContainer.BuildUp(t, existing, name, resolverOverrides);
        }

        public void Teardown(object o)
        {
            _unityContainer.Teardown(o);
        }

        public IUnityContainer AddExtension(UnityContainerExtension extension)
        {
            return _unityContainer.AddExtension(extension);
        }

        public object Configure(Type configurationInterface)
        {
            return _unityContainer.Configure(configurationInterface);
        }

        public IUnityContainer RemoveAllExtensions()
        {
            return _unityContainer.RemoveAllExtensions();
        }

        public IUnityContainer CreateChildContainer()
        {
            return _unityContainer.CreateChildContainer();
        }

        public IUnityContainer Parent
        {
            get { return _unityContainer.Parent; }
        }

        public IEnumerable<ContainerRegistration> Registrations
        {
            get { return _unityContainer.Registrations; }
        }

        private static InjectionMember[] GetInjectionMembersAndLogging(InjectionMember[] injectionMembers)
        {
            foreach (var injectionMember in injectionMembers)
            {
                if (injectionMember.GetType() == typeof (Interceptor<>) &&
                    injectionMember.GetType().GenericTypeArguments.Length > 0 &&
                    injectionMember.GetType().GenericTypeArguments[0] == typeof (InterfaceInterceptor))
                {
                    var allInjectionMembersPreExistingInteceptor =
                        new InjectionMember[]
                        {
                            new InterceptionBehavior<LoggingInterceptor>()
                        }.Concat(
                            injectionMembers).ToArray();
                    return allInjectionMembersPreExistingInteceptor;
                }
            }
            var allInjectionMembers =
                new InjectionMember[]
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<LoggingInterceptor>()
                }.Concat(
                    injectionMembers).ToArray();
            return allInjectionMembers;
        }
    }
}
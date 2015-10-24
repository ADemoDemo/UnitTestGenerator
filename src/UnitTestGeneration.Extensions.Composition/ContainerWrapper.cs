using SimpleInjector;
using System;
using System.Collections.Generic;

namespace UnitTestGeneration.Extensions.Composition
{
    class ContainerWrapper : IContainer
    {
        private readonly Container container;
        private ISet<Type> registeredServices = new HashSet<Type>();

        public ContainerWrapper(Container container)
        {
            this.container = container;
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>(Lifestyle.Singleton);
            registeredServices.Add(typeof(TService));
        }

        public void RegisterWhenNotExists<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (registeredServices.Contains(typeof(TService)))
            {
                return;
            }
            Register<TService, TImplementation>();
        }

        public void Register<TService>(TService implementationInstance)
            where TService : class
        {
            container.Register<TService>(() => implementationInstance, Lifestyle.Singleton);
            registeredServices.Add(typeof(TService));
        }

        public void RegisterWhenNotExists<TService>(TService implementationInstance)
            where TService : class
        {
            if (registeredServices.Contains(typeof(TService)))
            {
                return;
            }
            Register<TService>(implementationInstance);
        }

        public IEnumerable<Type> GetRegisteredServices()
        {
            return registeredServices;
        }
    }
}

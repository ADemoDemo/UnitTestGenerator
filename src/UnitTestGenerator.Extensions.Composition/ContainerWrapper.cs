/* ****************************************************************************
 * Copyright 2015 Peter Csikós
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * ***************************************************************************/
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace UnitTestGenerator.Extensions.Composition
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

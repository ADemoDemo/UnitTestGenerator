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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTestGenerator
{
    /// <summary>
    /// Scans types in assembly and filters them according to criteria.
    /// </summary>
    public class AssemblyTraverser : IAssemblyTraverser, IAssemblyTraverserConfigurator
    {
        private Assembly targetAssembly;
        private IEnumerable<Type> excludingTypes = new Type[0];
        private readonly bool internalsVisible;
        private readonly Func<Type, bool> typeFilter;

        /// <summary>
        /// Initializes a new instance of the AssemblyTraverser class.
        /// </summary>
        /// <param name="targetAssembly">Assembly to be traversed.</param>
        /// <param name="typeFilter">Func to filter types.</param>
        /// <param name="isInternalsVisible">Indicates that the consumer can access <paramref name="targetAssembly"/> internal members.</param>
        public AssemblyTraverser(Assembly targetAssembly,
           Func<Type, bool> typeFilter,
           bool isInternalsVisible)
        {
            Check.NotNull(targetAssembly, nameof(targetAssembly));
            Check.NotNull(typeFilter, nameof(typeFilter));

            this.targetAssembly = targetAssembly;
            this.typeFilter = typeFilter;
            this.internalsVisible = isInternalsVisible;
        }

        /// <summary>
        /// Returns collection of types.
        /// </summary>
        /// <returns>Collection of types.</returns>
        public IEnumerable<Type> GetTypes()
        {
            return targetAssembly.GetTypes().Where(x => (x.IsPublic || x.IsNotPublic && internalsVisible)
                        && typeFilter(x)
                        && !excludingTypes.Any(ex => ex == x)).ToArray();
        }

        /// <summary>
        /// Produces an instance of the AssemblyTraverser class.
        /// </summary>
        /// <param name="targetAssembly">Assembly to be traversed.</param>
        /// <param name="callingAssemblyName">Consumer assembly name to determine if internals are visible.</param>
        /// <returns>An instance of <see cref="AssemblyTraverser"/>.</returns>
        public static AssemblyTraverser Create(Assembly targetAssembly, string callingAssemblyName)
        {
            return new AssemblyTraverser(targetAssembly,
                 DefaultTypeFilter,
                 InternalsVisible(targetAssembly, callingAssemblyName));
        }

        public static bool InternalsVisible(Assembly targetAssembly, string callingAssemblyName)
        {
            Check.NotNull(targetAssembly, "targetAssembly");
            Check.NotNull(callingAssemblyName, "callingAssemblyName");
            return targetAssembly.GetCustomAttributes<System.Runtime.CompilerServices.InternalsVisibleToAttribute>().Any(x => x.AssemblyName == callingAssemblyName);
            //return false;
        }

        void IAssemblyTraverserConfigurator.Exclude(IEnumerable<Type> excluding)
        {
            this.excludingTypes = excluding.Concat(excludingTypes).Distinct().ToArray();
        }

        bool IAssemblyTraverser.InternalsVisible
        {
            get
            {
                return internalsVisible;
            }
        }

        private static bool DefaultTypeFilter(Type x)
        {
            return x.IsClass
                        && !x.IsNested
                        && !x.IsAbstract
                        && !x.IsGenericType;
        }

        private static IEnumerable<Type> GetBaseTypes(Type t)
        {
            var baseType = t;
            while ((baseType = baseType.BaseType) != null && baseType != typeof(object))
            {
                yield return baseType;
            }
        }
    }
}

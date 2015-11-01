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
using System.Reflection;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.ExpressionProviders;

namespace UnitTestGenerator.Extensions.Composition
{
    /// <summary>
    /// Extension methods for composing UnitTestBuilder.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Compose an instance of TestClassBuilder class.
        /// </summary>
        /// <param name="assembly">The assembly which types should be used.</param>
        /// <param name="callingAssemblyName">Name of calling assembly, which is set in <paramref name="assembly"/> in attribute InternalsVisibleTo.</param>
        /// <param name="mockProvider">An instance of <see cref="IMockExpressionProvider"/> for generating expressions of values of complex type.</param>
        /// <param name="valueExpressionProvider">An instance of <see cref="IValueExpressionProvider"/> for generating expressions of values of primitive types.</param>
        /// <returns>The instance of <see cref="ITestClassBuilder"/>.</returns>
        public static ITestClassBuilder ComposeTestClassBuilder(this Assembly assembly, 
            string callingAssemblyName, 
            IMockExpressionProvider mockProvider = null, 
            IValueExpressionProvider valueExpressionProvider = null)
        {
            return ComposeTestClassBuilder(assembly, callingAssemblyName, mockProvider, valueExpressionProvider, null);
        }

        /// <summary>
        /// Compose an instance of TestClassBuilder class.
        /// </summary>
        /// <param name="assembly">The assembly which types should be used.</param>
        /// <param name="callingAssemblyName">Name of calling assembly, which is set in <paramref name="assembly"/> in attribute InternalsVisibleTo.</param>
        /// <param name="mockProvider">An instance of <see cref="IMockExpressionProvider"/> for generating expressions of values of complex type.</param>
        /// <param name="valueExpressionProvider">An instance of <see cref="IValueExpressionProvider"/> for generating expressions of values of primitive types.</param>
        /// <param name="configure">Action where the configuration is set up.</param>
        /// <returns>The instance of <see cref="ITestClassBuilder"/>.</returns>
        public static ITestClassBuilder ComposeTestClassBuilder(this Assembly assembly,
            string callingAssemblyName,
            IMockExpressionProvider mockProvider = null,
            IValueExpressionProvider valueExpressionProvider = null,
            Action<ITestMethodGeneratorConfigurator> configure = null)
        {
            return ComposeTestClassBuilder(assembly, callingAssemblyName, container =>
            {
                if (mockProvider != null)
                { 
                    container.Register(mockProvider);
                }
                if (valueExpressionProvider != null)
                {
                    container.Register(valueExpressionProvider);
                }
            }, configure);
        }

        /// <summary>
        /// Compose an instance of TestClassBuilder class.
        /// </summary>
        /// <param name="assembly">The assembly which types should be used.</param>
        /// <param name="callingAssemblyName">Name of calling assembly, which is set in <paramref name="assembly"/> in attribute InternalsVisibleTo.</param>
        /// <param name="typeRegistration">The action where the type registration is set up.</param>
        /// <param name="configure">The action where the configuration is set up.</param>
        /// <returns>The instance of <see cref="ITestClassBuilder"/>.</returns>
        public static ITestClassBuilder ComposeTestClassBuilder(this Assembly assembly,
            string callingAssemblyName,
            Action<IContainer> typeRegistration,
            Action<ITestMethodGeneratorConfigurator> configure = null)
        {
            var assemblyTraverser = AssemblyTraverser.Create(assembly, callingAssemblyName);
            var generatorRegistrationManager = new GeneratorRegistrationManager();
            var testGeneratorConfigurator = new TestMethodGeneratorConfigurator(assemblyTraverser, generatorRegistrationManager);
            var setup = new TestMethodGeneratorComposition(assemblyTraverser, testGeneratorConfigurator, generatorRegistrationManager);
            var testGenerators = setup.GetGenerators(typeRegistration, configure);
            return new TestClassBuilder(assemblyTraverser, testGenerators);
        }
    }
}

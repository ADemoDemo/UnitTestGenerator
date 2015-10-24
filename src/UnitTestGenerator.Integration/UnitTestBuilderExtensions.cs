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
using System.Reflection;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.CodeGeneration.Generators;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGenerator.Integration
{
    //TODO: Type and method filter for generators
    //TODO: namespace imports

    public static class UnitTestBuilderExtensions
    {
        public static ITestBuilderConfigurator UseBuiltinGenerators(this ITestBuilderConfigurator configurator)
        {
            var testGeneratorConfigurator = configurator as TestGeneratorConfigurator;

            configurator.AddGenerator<NullArgumentMethodTestMethodGenerator>()
                .AddGenerator<NullArgumentConstructorTestMethodGenerator>();

            return configurator;
        }

        public static ITestClassBuilder Scan(this Assembly assembly, string callingAssemblyName, IMockExpressionProvider mockProvider, IValueExpressionProvider valueExpressionProvider)
        {
            return Scan(assembly, callingAssemblyName, mockProvider, valueExpressionProvider, null);
        }

        public static ITestClassBuilder Scan(this Assembly assembly,
            string callingAssemblyName,
            IMockExpressionProvider mockProvider,
            IValueExpressionProvider valueExpressionProvider,
            Action<ITestBuilderConfigurator> configure)
        {
            return Scan(assembly, callingAssemblyName, container =>
            {
                container.Register(mockProvider);
                container.Register(valueExpressionProvider);
            }, configure);
        }

        public static ITestClassBuilder Scan(this Assembly assembly,
            string callingAssemblyName,
            Action<IContainer> typeRegistration,
            Action<ITestBuilderConfigurator> configure)
        {
            var assemblyTraverser = AssemblyTraverser.Create(assembly, callingAssemblyName);
            var generatorRegistrationManager = new GeneratorRegistrationManager();
            var testGeneratorConfigurator = new TestGeneratorConfigurator(assemblyTraverser, generatorRegistrationManager);
            var setup = new TestMethodGeneratorComposition(assemblyTraverser, testGeneratorConfigurator, generatorRegistrationManager);
            var testGenerators = setup.GetGenerators(typeRegistration, configure);
            return new TestClassBuilder(assemblyTraverser, testGenerators);
        }
    }
}

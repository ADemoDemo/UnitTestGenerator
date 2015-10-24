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
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGenerator.Extensions.Composition
{
    /// <summary>
    /// Extensions methods for TestMethodGeneratorConfigurator.
    /// </summary>
    public static class ITestMethodGeneratorConfiguratorExtensions
    {
        /// <summary>
        /// Includes to configuration two of the default TestMethodGenerator(<see cref="NullArgumentMethodTestMethodGenerator"/>, <see cref="NullArgumentConstructorTestMethodGenerator"/>).
        /// </summary>
        /// <param name="configurator">The instance of configuration.</param>
        /// <returns>The instance of configuration.</returns>
        public static ITestMethodGeneratorConfigurator IncludeBuiltinGenerators(this ITestMethodGeneratorConfigurator configurator)
        {
            var testGeneratorConfigurator = configurator as TestMethodGeneratorConfigurator;

            configurator.AddGenerator<NullArgumentMethodTestMethodGenerator>()
                .AddGenerator<NullArgumentConstructorTestMethodGenerator>();

            return configurator;
        }

        /// <summary>
        /// Includes <see cref="NullArgumentMethodTestMethodGenerator"/> to configuration.
        /// </summary>
        /// <param name="configurator">The instance of configuration.</param>
        /// <returns>The instance of configuration.</returns>
        public static ITestMethodGeneratorConfigurator IncludeNullArgumentMethodTestMethodGenerator(this ITestMethodGeneratorConfigurator configurator)
        {
            configurator.AddGenerator<NullArgumentMethodTestMethodGenerator>();
            return configurator;
        }

        /// <summary>
        /// Includes <see cref="NullArgumentConstructorTestMethodGenerator"/> to configuration.
        /// </summary>
        /// <param name="configurator">The instance of configuration.</param>
        /// <returns>The instance of configuration.</returns>
        public static ITestMethodGeneratorConfigurator IncludeNullArgumentConstructorTestMethodGenerator(this ITestMethodGeneratorConfigurator configurator)
        {
            configurator.AddGenerator<NullArgumentConstructorTestMethodGenerator>();
            return configurator;
        }
    }
}

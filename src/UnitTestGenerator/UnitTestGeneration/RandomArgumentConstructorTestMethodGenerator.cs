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
using UnitTestGenerator.CodeGeneration.Generators;

namespace UnitTestGenerator.UnitTestGeneration
{
    /// <summary>
    /// Provides generation of collection of <see cref="TestMethod"/> based on passed <see cref="Type"/>. Generated methods initializes new instance of passed type with random values as parameters in constructor.
    /// </summary>
    public class RandomArgumentConstructorTestMethodGenerator : ConstructorAbstractTestMethodGenerator
    {
        private IEnumerable<Type> ignoreTypes;
        private readonly RandomArgumentConstructorTestMethodSourceCodeGenerator sourceCodeGenerator;

        /// <summary>
        /// Initializes a new instance of the RandomArgumentConstructorTestMethodGenerator class.
        /// </summary>
        /// <param name="ignoreTypes">Collection of types which should be ignored.</param>
        /// <param name="randomArgumentConstructorTestMethodSourceCodeGenerator">Source code generator.</param>
        public RandomArgumentConstructorTestMethodGenerator(IEnumerable<Type> ignoreTypes,
            RandomArgumentConstructorTestMethodSourceCodeGenerator randomArgumentConstructorTestMethodSourceCodeGenerator)
        {
            Check.NotNull(ignoreTypes, "ignoreTypes");
            Check.NotNull(randomArgumentConstructorTestMethodSourceCodeGenerator, "randomArgumentConstructorTestMethodSourceCodeGenerator");

            this.ignoreTypes = ignoreTypes;
            sourceCodeGenerator = randomArgumentConstructorTestMethodSourceCodeGenerator;
        }

        protected override IEnumerable<TestMethod> GenerateTestMethodsFor(ConstructorMetadata metadata)
        {
            var ctor = metadata.Constructor;
            if (IsDefaultConstructor(ctor) || !ConstructorIsAccessible(ctor) || TypeIsInIgnoreList(ctor))
            {
                return Enumerable.Empty<TestMethod>();
            }
            //var sourceCodeGenerator = new RandomArgumentConstructorTestMethodSourceCodeGenerator(null);

            var request = new ConstructorSourceCodeGenerationRequest(ctor, false, metadata.HasMultipleConstructors);
            var sourceCode = sourceCodeGenerator.BuildSourceCode(request);
            var methodName = sourceCodeGenerator.BuildMethodName(request);

            var testMethod = new TestMethod(ctor, methodName, sourceCode);
            return new[] { testMethod };
        }

        private bool TypeIsInIgnoreList(ConstructorInfo ctor)
        {
            return ignoreTypes.Any(x => x == ctor.DeclaringType);
        }

        private static bool ConstructorIsAccessible(ConstructorInfo ctor)
        {
            return !ctor.DeclaringType.IsAbstract && (ctor.IsPublic || ctor.IsAssembly || ctor.IsFamilyOrAssembly);
        }
    }
}

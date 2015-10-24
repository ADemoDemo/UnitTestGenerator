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
    /// Provides generation of an enumeration of <see cref="TestMethod"/> based on passed <see cref="Type"/>. Each generated test method try to initialize a new instance of passed type. Tested constructor must have at least one parameter which is nullable. Each test validates only one parameter for <see cref="ArgumentNullException"/>.
    /// </summary>
    public class NullArgumentConstructorTestMethodGenerator : ConstructorAbstractTestMethodGenerator
    {
        private readonly INullArgumentConstructorTestMethodSourceCodeGenerator sourceCodeGenerator;

        /// <summary>
        /// Initializes a new instance of the NullArgumentConstructorTestMethodGenerator class.
        /// </summary>
        /// <param name="nullArgumentMethodTestMethodSourceCodeGenerator">Source code generator.</param>
        public NullArgumentConstructorTestMethodGenerator(INullArgumentConstructorTestMethodSourceCodeGenerator nullArgumentConstructorTestMethodSourceCodeGenerator)
        {
            Check.NotNull(nullArgumentConstructorTestMethodSourceCodeGenerator, "nullArgumentConstructorTestMethodSourceCodeGenerator");
            this.sourceCodeGenerator = nullArgumentConstructorTestMethodSourceCodeGenerator;
        }

        protected override IEnumerable<TestMethod> GenerateTestMethodsFor(ConstructorMetadata metadata)
        {
            if (IsDefaultConstructor(metadata.Constructor))
            {
                return Enumerable.Empty<TestMethod>();
            }

            var methods = new List<TestMethod>();
            foreach (var nullParameter in metadata.Constructor.GetParameters().Where(x => ParameterSatisfied(x)))
            {
                var request = new ConstructorSourceCodeGenerationRequest(metadata.Constructor, 
                    metadata.NullArgumentNeedsExplicitCast,
                    metadata.HasMultipleConstructors,
                    nullParameter);
                var sourceCode = sourceCodeGenerator.BuildSourceCode(request);
                var method = new TestMethod(metadata.Constructor, sourceCodeGenerator.BuildMethodName(request), sourceCode, typeof(ArgumentNullException));
                methods.Add(method);
            }
            return methods;
        }

        public static bool ParameterSatisfied(ParameterInfo parameter)
        {
            Check.NotNull(parameter, nameof(parameter));
            return !(parameter.HasDefaultValue || !parameter.ParameterType.IsNullable());
        }
    }
}

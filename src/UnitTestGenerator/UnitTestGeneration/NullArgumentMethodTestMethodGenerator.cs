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
    /// Provides generation of an enumeration of <see cref="TestMethod"/> based on passed <see cref="Type"/>. Each generated test method calls one tested method on an instance of passed type. Tested method must have at least one parameter which is nullable. Each test validates only one parameter for <see cref="ArgumentNullException"/>.
    /// </summary>
    public class NullArgumentMethodTestMethodGenerator : MethodAbstractTestMethodGenerator
    {
        private readonly INullArgumentMethodTestMethodSourceCodeGenerator sourceCodeGenerator;

        /// <summary>
        /// Initializes a new instance of the NullArgumentMethodTestMethodGenerator class.
        /// </summary>
        /// <param name="nullArgumentMethodTestMethodSourceCodeGenerator">Source code generator.</param>
        public NullArgumentMethodTestMethodGenerator(INullArgumentMethodTestMethodSourceCodeGenerator nullArgumentMethodTestMethodSourceCodeGenerator)
        {
            Check.NotNull(nullArgumentMethodTestMethodSourceCodeGenerator, "nullArgumentMethodTestMethodSourceCodeGenerator");
            sourceCodeGenerator = nullArgumentMethodTestMethodSourceCodeGenerator;
        }

        protected override IEnumerable<TestMethod> GenerateTestMethodsFor(MethodMetadata metadata)
        {
            var tests = new List<TestMethod>();

            var parameters = metadata.Method.GetParameters();
            foreach (var nullParameter in parameters.Where(x => ParameterSatisfied(x)))
            {
                var request = new MethodSourceCodeGenerationRequest(metadata.Method, metadata.NullArgumentNeedsExplicitCast, nullParameter);
                var sourceCode = sourceCodeGenerator.BuildSourceCode(request);
                string methodName = sourceCodeGenerator.BuildMethodName(request);

                var testMethod = new TestMethod(metadata.Method, methodName, sourceCode, typeof(ArgumentNullException));
                tests.Add(testMethod);
            }

            return tests;
        }

        public static bool ParameterSatisfied(ParameterInfo parameter)
        {
            Check.NotNull(parameter, nameof(parameter));
            return !(parameter.HasDefaultValue || !parameter.ParameterType.IsNullable());
        }
    }
}

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
namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Provides source code generation for constructor invoke populated with random value arguments.
    /// </summary>
    public class RandomArgumentConstructorTestMethodSourceCodeGenerator : ConstructorTestMethodSourceCodeGenerator
    {
        /// <summary>
        /// Initializes a new instance of the RandomArgumentConstructorTestMethodSourceCodeGenerator class.
        /// </summary>
        /// <param name="expressionBuilder">The expression builder.</param>
        public RandomArgumentConstructorTestMethodSourceCodeGenerator(IExpressionBuilder expressionBuilder)
            : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Builds the method name.
        /// </summary>
        /// <param name="request">The object containing information about invoked constructor.</param>
        /// <returns>The generated string containing name of method.</returns>
        public override string BuildMethodName(ConstructorSourceCodeGenerationRequest request)
        {
            Check.NotNull(request, "request");
            var ctorName = BuildConstructorName(request);
            return request.Constructor.DeclaringType.Name + "_Constructor" + ctorName;
        }
    }
}

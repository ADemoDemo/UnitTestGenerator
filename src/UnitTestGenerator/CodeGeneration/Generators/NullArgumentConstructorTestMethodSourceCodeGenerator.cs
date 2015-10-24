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
    /// Provides the methods that are required for generating source code of a test method which calls the tested constructor.
    /// </summary>
    public class NullArgumentConstructorTestMethodSourceCodeGenerator : ConstructorTestMethodSourceCodeGenerator, INullArgumentConstructorTestMethodSourceCodeGenerator
    {
        readonly IExpressionBuilder expressionBuilder;

        /// <summary>
        /// Initializes a new instance of the XXX class.
        /// </summary>
        /// <param name="expressionBuilder">The expression builder.</param>
        public NullArgumentConstructorTestMethodSourceCodeGenerator(IExpressionBuilder expressionBuilder)
            : base(expressionBuilder)
        {
            this.expressionBuilder = expressionBuilder;
        }

        protected override void BuildActSourceCode(ConstructorSourceCodeGenerationRequest request)
        {
            var createInstanceExpr = CreateCtorExpression(request.Constructor, request.ParameterDestinedAsNull, request.NullArgumentNeedsExplicitCast);
            Append(expressionBuilder.ExpressionToString(createInstanceExpr));
            AppendLine(";");
        }

        protected override void BuildAssertSourceCode(ConstructorSourceCodeGenerationRequest request)
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

            string ctorName = BuildConstructorName(request);
            return request.Constructor.DeclaringType.Name + "_Constructor_" + ctorName + request.ParameterDestinedAsNull.Name.Capitalize() + "NullValueGiven_ShouldThrowArgumentNullException";
        }
    }
}

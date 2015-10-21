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
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace UnitTestGenerator.CodeGeneration.Generators
{
    /// <summary>
    /// Provides the methods that are required for generating source code of a test method which calls the tested method.
    /// </summary>
    public class NullArgumentMethodTestMethodSourceCodeGenerator : AbstractTestMethodSourceCodeGenerator<MethodSourceCodeGenerationRequest>, INullArgumentMethodTestMethodSourceCodeGenerator
    {
        private readonly ITestMethodValueProvider testMethodValueProvider;
        readonly IExpressionBuilder expressionBuilder;

        /// <summary>
        /// Initializes a new instance of the NullArgumentMethodTestMethodSourceCodeGenerator class.
        /// </summary>
        /// <param name="expressionBuilder">The expression builder.</param>
        /// <param name="testMethodValueProvider">The value provider.</param>
        public NullArgumentMethodTestMethodSourceCodeGenerator(IExpressionBuilder expressionBuilder,
            ITestMethodValueProvider testMethodValueProvider)
        {
            Check.NotNull(expressionBuilder, nameof(expressionBuilder));
            Check.NotNull(testMethodValueProvider, nameof(testMethodValueProvider));
            this.expressionBuilder = expressionBuilder;
            this.testMethodValueProvider = testMethodValueProvider;
        }

        protected ParameterExpression InstanceExpression { get; set; }

        /// <summary>
        /// Builds the method name.
        /// </summary>
        /// <param name="request">The object containing information about invoked method.</param>
        /// <returns>The generated string containing name of method.</returns>
        public override string BuildMethodName(MethodSourceCodeGenerationRequest request)
        {
            Check.NotNull(request, "request");
            return request.Method.Name + "_" + request.ParameterDestinedAsNull.Name.Capitalize() + "NullValueGiven_ShouldThrowArgumentNullException";
        }

        protected override void BuildActSourceCode(MethodSourceCodeGenerationRequest request)
        {
            var method = request.Method;

            ////var paramExpression = Expression.Parameter(method.ReflectedType, "x");
            //var arguments = CreateArgumentExpressions(request.TargetParameter, method.GetParameters(), request.NullArgumentNeedsExplicitCast);
            var arguments = expressionBuilder.CreateArgumentExpressions(method.GetParameters(), request.NullArgumentNeedsExplicitCast, request.ParameterDestinedAsNull);
            var callExpression = method.IsStatic ? Expression.Call(method, arguments) : Expression.Call(InstanceExpression, method, arguments);
            //var callExpression = Expression.Call(InstanceExpression, method, arguments);
            ////var lambda = Expression.Lambda(typeof(Action<>).MakeGenericType(method.ReflectedType), callExpression, paramExpression);
            ////var invokeGenericMethod = ((MethodCallExpression)invokingActionMethod.Body).Method.GetGenericMethodDefinition().MakeGenericMethod(method.ReflectedType);
            ////var invokeExpr = Expression.Call(null, invokeGenericMethod, InstanceExpression, lambda);

            Append(expressionBuilder.ExpressionToString(callExpression));
            //sb.Append("\t");
            //sb.AppendFormat(@".ShouldThrowExactly<ArgumentNullException>().Where(x => x.ParamName == ""{0}"")", nullParameter.Name);
            AppendLine(";");
            //sb.AppendLine(GetValidationCode(nullParameter));
            //var sourceCode = sb.ToString();
            //return sourceCode;
        }

        protected override void BuildArrangeSourceCode(MethodSourceCodeGenerationRequest request)
        {
            var method = request.Method;
            if (method.IsStatic)
            {
                return;
            }
            //StringBuilder testInitializationSourceCode = new StringBuilder();
            //ParameterExpression varExpr;
            string variableName;
            if (testMethodValueProvider.HasVariableForTestedType(method.ReflectedType, out variableName))//referencing existing variable
            {
                InstanceExpression = Expression.Variable(method.ReflectedType, variableName);
            }
            else
            {
                ParameterExpression instanceExpression;
                var assignExpr = expressionBuilder.BuildInstanceCreationWithAssigmentExpression(method.ReflectedType, out instanceExpression);
                InstanceExpression = instanceExpression;
                Append("var ");
                var expresionAsString = expressionBuilder.ExpressionToString(assignExpr);
                Append(expresionAsString.RemoveParantheses());
                AppendLine(";");
            }
        }
    }
}

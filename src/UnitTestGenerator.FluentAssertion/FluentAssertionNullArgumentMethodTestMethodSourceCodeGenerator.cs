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
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator.CodeGeneration.Generators;

namespace UnitTestGenerator.FluentAssertion
{
    public class FluentAssertionNullArgumentMethodTestMethodSourceCodeGenerator : NullArgumentMethodTestMethodSourceCodeGenerator
    {
        readonly IFluentAssertionExpressionBuilder expressionBuilder;

        public FluentAssertionNullArgumentMethodTestMethodSourceCodeGenerator(IFluentAssertionExpressionBuilder expressionBuilder, 
            ITestMethodValueProvider testMethodValueProvider) 
            : base(expressionBuilder, testMethodValueProvider)
        {
            this.expressionBuilder = expressionBuilder;
        }

        //protected override Expression BuildCallExpression(MethodSourceCodeGenerationRequest request)
        //{
        //    var method = request.Method;
        //    var callExpression = base.BuildCallExpression(request);

        //    var lambda = Expression.Lambda(typeof(Action<>).MakeGenericType(method.ReflectedType), callExpression, paramExpression);
        //    var invokeGenericMethod = ((MethodCallExpression)invokingActionMethod.Body).Method.GetGenericMethodDefinition().MakeGenericMethod(method.ReflectedType);
        //    var invokeExpr = Expression.Call(null, invokeGenericMethod, InstanceExpression, lambda);

        //}

        protected override void BuildActSourceCode(MethodSourceCodeGenerationRequest request)
        {
            var method = request.Method;

            var arguments = expressionBuilder.CreateArgumentExpressions(method.GetParameters(), request.NullArgumentNeedsExplicitCast, request.ParameterDestinedAsNull);
            MethodCallExpression invokeExpr = expressionBuilder.BuildFluentAssertionMethodInvokingExpression(method, InstanceExpression, arguments);

            Append(expressionBuilder.ExpressionToString(invokeExpr));
            AppendLine(";");
        }

        //private MethodCallExpression BuildFluentAssertionMethodInvokingExpression(MethodInfo method, ParameterExpression paramExpression, IEnumerable<Expression> arguments)
        //{
        //}
    }
}

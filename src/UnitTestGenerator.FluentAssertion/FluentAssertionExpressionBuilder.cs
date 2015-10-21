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
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.CodeGeneration.Generators;

namespace UnitTestGenerator.FluentAssertion
{
    class FluentAssertionExpressionBuilder : ExpressionBuilder, IFluentAssertionExpressionBuilder
    {
        Expression<Func<object, Action<object>, Action>> invokingActionMethod = (instance, action) => AssertionExtensions.Invoking(instance, action);

        public FluentAssertionExpressionBuilder(ITestMethodValueProvider testMethodValueProvider, IMockExpressionProvider mockProvider, IValueExpressionProvider valueExpressionProvider, IIdentifierValidator identifierValidator)
            : base(testMethodValueProvider, mockProvider, valueExpressionProvider, identifierValidator)
        {
        }

        //public FluentAssertionExpressionBuilder(IExpressionBuilder parameterExpressionBuilder,
        //    IIdentifierValidator identifierValidator)
        //    : base(identifierValidator)
        //{
        //}

        public MethodCallExpression BuildFluentAssertionMethodInvokingExpression(MethodInfo method, 
            Expression instanceExpression,
            IEnumerable<Expression> arguments)
        {
            var paramExpression = Expression.Parameter(method.ReflectedType, "x");
            if (method.IsStatic)
            {
                //AppendLine("//TODO: implement static method");
                //return;
            }
            var callExpression = Expression.Call(paramExpression, method, arguments);
            var lambda = Expression.Lambda(typeof(Action<>).MakeGenericType(method.ReflectedType), callExpression, paramExpression);
            var invokeGenericMethod = ((MethodCallExpression)invokingActionMethod.Body).Method.GetGenericMethodDefinition().MakeGenericMethod(method.ReflectedType);
            var invokeExpr = Expression.Call(null, invokeGenericMethod, instanceExpression, lambda);
            return invokeExpr;
        }
    }
}

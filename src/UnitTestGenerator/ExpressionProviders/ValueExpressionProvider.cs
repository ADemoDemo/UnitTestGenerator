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
using System.Linq.Expressions;
using System.Reflection;
using UnitTestGenerator.CodeGeneration;

namespace UnitTestGenerator.ExpressionProviders
{
    /// <summary>
    /// Provides Expression for obtaining primitive values using <see cref="Value"/> class.
    /// </summary>
    public class ValueExpressionProvider : IValueExpressionProvider
    {
        private readonly MethodInfo createmethod;

        /// <summary>
        /// Initializes a new instance of the ValueExpressionProvider class.
        /// </summary>
        public ValueExpressionProvider()
        {
            createmethod = ExtractGenericMethod(() => Value.Create<string>());
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> which calls Create method on <see cref="Value"/>.
        /// </summary>
        /// <param name="forType">The type for which the expression is created.</param>
        /// <returns>The created expression.</returns>
        public Expression CreateValueExpression(Type forType)
        {
            var genericMethod = createmethod.MakeGenericMethod(forType);
            return Expression.Call(genericMethod);
        }

        private static MethodInfo ExtractGenericMethod(Expression<Func<string>> stringValueCreator)
        {
            var callExpression = stringValueCreator.Body as MethodCallExpression;
            if (callExpression == null)
            {
                throw new ArgumentException("Passed Expression should be a method call like () => Provider.CreateValue<string>().", "stringValueCreator");
            }
            if (!callExpression.Method.IsGenericMethod)
            {
                throw new ArgumentException("Method call used in passed Expression should be a call of a generic method like () => Provider.CreateValue<string>().", "stringValueCreator");
            }
            return callExpression.Method.GetGenericMethodDefinition();
        }
    }
}

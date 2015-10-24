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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator.CodeGeneration;

namespace UnitTestGenerator.ExpressionProviders
{
    public class AutoFixtureValueExpressionProvider : IValueExpressionProvider
    {
        private readonly MethodInfo createmethod;

        public AutoFixtureValueExpressionProvider()
        {
            createmethod = ExtractGenericMethod(() => Value.Create<string>());
        }

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

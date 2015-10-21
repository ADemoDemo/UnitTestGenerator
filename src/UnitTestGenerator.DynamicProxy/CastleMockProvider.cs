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
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator.CodeGeneration;

namespace UnitTestGenerator.DynamicProxy
{
    public class CastleMockProvider : IMockExpressionProvider
    {
        public Expression CreateMockExpression(Type forType)
        {
            Func<CastleMockProvider> func = ProxyGenerator.CreateProxy<CastleMockProvider>;
            var method = func.Method.GetGenericMethodDefinition();
            //var proxyGeneratorType = typeof(ProxyGenerator);
            //var method = proxyGeneratorType.GetMethods().Single(x => x.IsGenericMethod);
            var genericMethod = method.MakeGenericMethod(forType);
            return Expression.Call(null, genericMethod);
        }
    }
}

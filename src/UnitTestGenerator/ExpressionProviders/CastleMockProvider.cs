using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.DynamicProxy;

namespace UnitTestGenerator.ExpressionProviders
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

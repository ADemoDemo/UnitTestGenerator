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

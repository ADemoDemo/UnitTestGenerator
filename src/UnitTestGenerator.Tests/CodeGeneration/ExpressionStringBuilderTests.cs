using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using FluentAssertions;

namespace UnitTestGenerator.Tests
{
    public partial class ExpressionStringBuilderTests
    {
        [TestMethod]
        public void ExpressionToString_StaticMethodCallExpressionGiven_ShouldProduceApropriateStringRepresentation()
        {
            string expected = "ProxyGenerator.CreateProxy<Hashtable>()";

            Expression<Func<Hashtable>> expression = () => UnitTestGenerator.DynamicProxy.ProxyGenerator.CreateProxy<Hashtable>();
            var result = ExpressionStringBuilder.ExpressionToString(expression.Body);

            result.Should().Be(expected);
        }

        [TestMethod]
        public void ExpressionToString_CastFromNullExpressionGiven_ShouldProduceApropriateStringRepresentation()
        {
            string expected = "(Hashtable)null";

            //Expression<Func<IEnumerable>> expression = () => (IEnumerable)(object)5;
            var expression = Expression.Convert(Expression.Constant(null), typeof(Hashtable));

            var result = ExpressionStringBuilder.ExpressionToString(expression);

            result.Should().Be(expected);
        }


        [TestMethod]
        public void ExpressionToString_Constant_ShouldProduceApropriateStringRepresentation()
        {
            string expected = "true";

            var expression = Expression.Constant(true);

            var result = ExpressionStringBuilder.ExpressionToString(expression);

            result.Should().Be(expected);
        }

        [TestMethod]
        public void ExpressionToString_TypeOf_ShouldProduceApropriateStringRepresentation()
        {
            string expected = "typeof(string)";

            Expression<Func<Type>> expression = () => typeof(string);

            var result = ExpressionStringBuilder.ExpressionToString(expression.Body);

            result.Should().Be(expected);
        }

        [TestMethod]
        public void ExpressionToString_ComplexGenericType_ShouldProduceApropriateStringRepresentation()
        {
            string expected = "(Expression<Func<string>>)(() => string.Empty)";
            LambdaExpression expression = (Expression<Func<LambdaExpression>>)(() => (Expression<Func<string>>)(() => string.Empty));

            var result = ExpressionStringBuilder.ExpressionToString(expression.Body);

            result.Should().Be(expected);
        }

        [TestMethod]
        public void ExpressionToString_Assign_ShouldBeWithoutParentheses()
        {
            string expected = "variable = 5";
            Expression expression = Expression.Assign(Expression.Variable(typeof(int), "variable"), Expression.Constant(5));

            var result = ExpressionStringBuilder.ExpressionToString(expression);

            result.Should().Be(expected);
        }
    }
}
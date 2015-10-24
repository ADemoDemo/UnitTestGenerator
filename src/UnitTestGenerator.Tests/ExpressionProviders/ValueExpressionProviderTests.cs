using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Linq.Expressions;

namespace UnitTestGenerator.ExpressionProviders.Tests
{
    [TestClass]
    public class ValueExpressionProviderTests
    {
        [TestMethod]
        public void CreateValueExpression_StringTypeGiven_ShouldReturnExpressionWithStringAsTypeParamater()
        {
            ValidateTypeExpressionReturned<string>();
        }

        [TestMethod]
        public void CreateValueExpression_IntTypeGiven_ShouldReturnExpressionWithIntAsTypeParamater()
        {
            ValidateTypeExpressionReturned<int>();
        }

        [TestMethod]
        public void CreateValueExpression_DecimalTypeGiven_ShouldReturnExpressionWithDecimalAsTypeParamater()
        {
            ValidateTypeExpressionReturned<decimal>();
        }

        private static void ValidateTypeExpressionReturned<TType>()
        {
            var testee = new ValueExpressionProvider();

            var result = testee.CreateValueExpression(typeof(TType));

            result.Should().BeAssignableTo<MethodCallExpression>();
            var methodExpression = (MethodCallExpression)result;
            methodExpression.Method.IsGenericMethod.Should().BeTrue();
            methodExpression.Method.GetGenericArguments().Should().ContainSingle().Which.Should().Be(typeof(TType));
        }
    }
}
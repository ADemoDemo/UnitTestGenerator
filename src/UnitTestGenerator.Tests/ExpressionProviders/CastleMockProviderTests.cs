using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator.ExpressionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FluentAssertions;

namespace UnitTestGenerator.ExpressionProviders.Tests
{
    [TestClass]
    public class CastleMockProviderTests
    {
        [TestMethod]
        public void CreateMockExpression_StringBuilderTypeGiven_ShouldCreateExpressionReturnStringBuilderInstance()
        {
            var castleMockProvider = new CastleMockProvider();

            var result = castleMockProvider.CreateMockExpression(typeof(StringBuilder)) as MethodCallExpression;

            result.Should().NotBeNull();
            result.Type.Should().Be(typeof(StringBuilder));
        }
    }
}
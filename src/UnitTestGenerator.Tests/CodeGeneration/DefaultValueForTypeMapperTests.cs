using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FluentAssertions;
using UnitTestGenerator.CodeGeneration;

namespace UnitTestGenerator.Tests.CodeGeneration
{
    public partial class DefaultValueForTypeMapperTests
    {
        private string expectedVariableName;
        private DefaultValueForTypeMapper testee;
        private Expression<Func<Type>> typeofStringExpression;

        [TestMethod]
        public void DefaultConstructor_Always_Succeeds()
        {
            new DefaultValueForTypeMapper();
        }

        [TestMethod()]
        public void HasExpressionForArgument_RegisteredTypeGiven_ShouldReturnTrueAndExpression()
        {
            Expression expression = null;
            var result = testee.HasExpressionForArgument(typeof(Type), out expression);

            result.Should().Be(true);
            expression.Should().Be(typeofStringExpression.Body);
        }


        [TestMethod()]
        public void HasExpressionForArgument_UnregisteredTypeGiven_ShouldReturnFalseAndNull()
        {
            Expression expression = null;
            var result = testee.HasExpressionForArgument(typeof(double), out expression);

            result.Should().Be(false);
            expression.Should().BeNull();
        }

        [TestMethod()]
        public void HasVariableForTestedType_RegisteredTypeGiven_ShouldReturnTrueAndVariableName()
        {
            string variableName = null;
            var result = testee.HasVariableForTestedType(typeof(string), out variableName);

            result.Should().Be(true);
            variableName.Should().Be(expectedVariableName);
        }

        [TestMethod()]
        public void HasVariableForTestedType_UnregisteredTypeGiven_ShouldReturnFalseAndNull()
        {
            string variableName = null;
            var result = testee.HasVariableForTestedType(typeof(double), out variableName);

            result.Should().Be(false);
            variableName.Should().BeNull();
        }

        [TestInitialize]
        public void Initialize()
        {
            expectedVariableName = "stringField";
            typeofStringExpression = (Expression<Func<Type>>)(() => typeof(string));
            var parameterMapping = new Dictionary<Type, string> { { typeof(string), expectedVariableName } };
            var defaultValues = new LambdaExpression[] { typeofStringExpression };
            testee = new DefaultValueForTypeMapper(parameterMapping, defaultValues);
        }
    }
}
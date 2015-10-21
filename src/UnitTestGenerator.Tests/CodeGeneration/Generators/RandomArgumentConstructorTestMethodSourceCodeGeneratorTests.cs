using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Rhino.Mocks;
using System.Linq.Expressions;
using FluentAssertions;
using TestAssembly;
using System.Linq;

namespace UnitTestGenerator.CodeGeneration.Generators.Tests
{
    [TestClass]
    public class RandomArgumentConstructorTestMethodSourceCodeGeneratorTests
    {
        private IExpressionBuilder expressionBuilder;
        private ConstructorSourceCodeGenerationRequest request;
        RandomArgumentConstructorTestMethodSourceCodeGenerator testee;

        [TestMethod]
        public void BuildMethodName()
        {
            var methodName = testee.BuildMethodName(request);

            methodName.Should().Be("ClassContructor_Constructor");
        }

        [TestMethod]
        public void BuildSourceCode()
        {
            var expected = "expr1";

            expressionBuilder.Stub(m => m.CreateArgumentExpressions(null))
                .IgnoreArguments()
                .Return(new[] { Expression.Constant(null, typeof(PublicClass)) });
            expressionBuilder.Stub(m => m.ExpressionToString(null))
                .IgnoreArguments()
                .Return(expected);

            var sourceCode = testee.BuildSourceCode(request);

            sourceCode.Should().Be("var "+ expected + ";" + Environment.NewLine);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var type = typeof(ClassContructor);
            var ctor = type.GetConstructors().First();
            request = new ConstructorSourceCodeGenerationRequest(ctor, false, false, ctor.GetParameters().First());
            expressionBuilder = MockRepository.GenerateMock<IExpressionBuilder>();
            testee = new RandomArgumentConstructorTestMethodSourceCodeGenerator(expressionBuilder);
        }
       }
}
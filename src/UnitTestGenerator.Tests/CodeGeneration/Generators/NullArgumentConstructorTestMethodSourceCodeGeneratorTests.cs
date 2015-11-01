using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator.CodeGeneration.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using TestAssembly;
using FluentAssertions;
using System.Linq.Expressions;

namespace UnitTestGenerator.CodeGeneration.Generators.Tests
{
    [TestClass]
    public class NullArgumentConstructorTestMethodSourceCodeGeneratorTests
    {
        private IExpressionBuilder expressionBuilder;
        private ConstructorSourceCodeGenerationRequest request;
        private NullArgumentConstructorTestMethodSourceCodeGenerator testee;

        [TestMethod]
        public void BuildMethodName()
        {
            var methodName =  testee.BuildMethodName(request);

            methodName.Should().Be("Constructor_PublicClassNullValueGiven_ShouldThrowArgumentNullException");
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

            sourceCode.Should().Be(expected + ";" + Environment.NewLine);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var type = typeof(ClassContructor);
            var ctor = type.GetConstructors().First();
            request = new ConstructorSourceCodeGenerationRequest(ctor, false, false, ctor.GetParameters().First());
            expressionBuilder = MockRepository.GenerateMock<IExpressionBuilder>();
            testee = new NullArgumentConstructorTestMethodSourceCodeGenerator(expressionBuilder);
        }
    }
}
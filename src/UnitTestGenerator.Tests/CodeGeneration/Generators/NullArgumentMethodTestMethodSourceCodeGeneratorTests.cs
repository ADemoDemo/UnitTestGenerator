using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq.Expressions;
using TestAssembly;

namespace UnitTestGenerator.CodeGeneration.Generators.Tests
{
    [TestClass]
    public class NullArgumentMethodTestMethodSourceCodeGeneratorTests
    {
        delegate void FooBarSignature(string input);
        delegate void EnumerableArgumentMethodSignature(IEnumerable<string> strings);

        private IExpressionBuilder expressionBuilder;
        private NullArgumentMethodTestMethodSourceCodeGenerator testee;
        private MethodSourceCodeGenerationRequest instanceMethodRequest;

        [TestMethod]
        public void BuildMethodName_MethodRequestGiven_ShouldProduceEquivalentName()
        {
            var methodName = testee.BuildMethodName(instanceMethodRequest);

            methodName.Should().Be("FooBar_InputNullValueGiven_ShouldThrowArgumentNullException");
        }

        [TestMethod]
        public void BuildMethodName_GenericArgumentMethodWithOverloadGiven_ShouldProduceEquivalentName()
        {
            EnumerableArgumentMethodSignature enumerableArgumentMethod = new TestAssembly.OverloadedMethods("").OverloadedMethod;
            var enumerableArgumentMethodRequest = new MethodSourceCodeGenerationRequest(enumerableArgumentMethod.Method, false, enumerableArgumentMethod.Method.GetParameters().First(), true);

            var methodName = testee.BuildMethodName(enumerableArgumentMethodRequest);

            methodName.Should().Be("OverloadedMethod_IEnumerableString_StringsNullValueGiven_ShouldThrowArgumentNullException");
        }

        [TestMethod]
        public void BuildSourceCode_InstanceMethodGiven_ShouldCallMethodOnInstance()
        {
            const string expressionString = "expr1";
            StubExpressionBuilder(expressionString);

            var sourceCode = testee.BuildSourceCode(instanceMethodRequest);

            sourceCode.Should().Be($@"var {expressionString};{Environment.NewLine}{expressionString};{Environment.NewLine}");
        }

        [TestMethod]
        public void BuildSourceCode_StaticMethodGiven_ShouldCallMethodWithoutInstance()
        {
            const string expressionString = "expr1";
            StubExpressionBuilder(expressionString);
            FooBarSignature staticMethod = PublicClass.StaticMethod;
            var staticMethodRequest = new MethodSourceCodeGenerationRequest(staticMethod.Method, false, staticMethod.Method.GetParameters().First());

            var sourceCode = testee.BuildSourceCode(staticMethodRequest);

            sourceCode.Should().Be($@"{expressionString};{Environment.NewLine}");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            FooBarSignature instanceMethod = new TestAssembly.PublicClass().FooBar;

            expressionBuilder = MockRepository.GenerateMock<IExpressionBuilder>();
            instanceMethodRequest = new MethodSourceCodeGenerationRequest(instanceMethod.Method, false, instanceMethod.Method.GetParameters().First());
            var testMethodValueProvider = MockRepository.GenerateMock<ITestMethodValueProvider>();
            testee = new NullArgumentMethodTestMethodSourceCodeGenerator(expressionBuilder, testMethodValueProvider);
        }

        private void StubExpressionBuilder(string expressionString)
        {
            ParameterExpression variableExpresion = Expression.Parameter(typeof(PublicClass), "parameter");
            expressionBuilder.Stub(m => m.BuildInstanceCreationWithAssigmentExpression(null, out variableExpresion))
                .IgnoreArguments()
                .OutRef(variableExpresion)
                .Return(Expression.Add(Expression.Constant(5), Expression.Constant(6)));
            expressionBuilder.Stub(m => m.ExpressionToString(null))
                .IgnoreArguments()
                .Return(expressionString);
            expressionBuilder.Stub(m => m.CreateArgumentExpressions(null))
                .IgnoreArguments()
                .Return(new[] { Expression.Constant("abc") });
        }
    }
}
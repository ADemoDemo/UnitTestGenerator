using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Rhino.Mocks;
using UnitTestGenerator.CodeGeneration.Generators;
using System.Linq.Expressions;
using FluentAssertions;


namespace UnitTestGenerator.FluentAssertion.Tests
{
    [TestClass]
    public class FluentAssertionNullArgumentMethodTestMethodSourceCodeGeneratorTests
    {
        private delegate void TestMethod(IEnumerable<string> strings);
        private delegate void VoidMethod();

//        [TestMethod]
//        public void BuildSourceCode()
//        {
//            var sampleInstance = new TestAssembly.OverloadedMethods("");
//            var targetType = sampleInstance.GetType();
//            var variableExpression = Expression.Parameter(targetType, "instance");

//            var expressionBuilder = MockRepository.GenerateMock<IFluentAssertionExpressionBuilder>();
//            ParameterExpression outExpression;
//            expressionBuilder.Stub(m => m.BuildInstanceInstantiationExpression(targetType, out outExpression))
//                .OutRef(variableExpression)
//                .Return(Expression.Assign(variableExpression, ((Expression<Action>)(() => new TestAssembly.OverloadedMethods(""))).Body));

//            expressionBuilder.Stub(m => m.CreateArgumentExpressions(null))
//                .IgnoreArguments()
//                .Return(new[] { Expression.NewArrayInit(typeof(string), Expression.Constant("abc")) });

//            var testee = new FluentAssertionNullArgumentMethodTestMethodSourceCodeGenerator(expressionBuilder,
//                MockRepository.GenerateMock<ITestMethodValueProvider>());
//            TestMethod methodDelegate = sampleInstance.OverloadedMethod;
//            var request = new MethodSourceCodeGenerationRequest(methodDelegate.Method);

//            var result = testee.BuildSourceCode(request);

//            result.Should().Be(@"var instance = new OverloadedMethods("""");
//instance.Invoking<OverloadedMethods>(x => x.OverloadedMethod(new [] {""abc""}));
//");
//        }

        [TestMethod]
        public void BuildSourceCode2()
        {
            const string expr = "expr1";
            var sampleInstance = new TestAssembly.OverloadedMethods("");
            TestMethod methodDelegate = sampleInstance.OverloadedMethod;
            var expressionBuilder = MockRepository.GenerateMock<IFluentAssertionExpressionBuilder>();
            expressionBuilder.Stub(m => m.ExpressionToString(null))
                .IgnoreArguments()
                .Return(expr);
            var testee = new FluentAssertionNullArgumentMethodTestMethodSourceCodeGenerator(expressionBuilder,
                MockRepository.GenerateMock<ITestMethodValueProvider>());
            var request = new MethodSourceCodeGenerationRequest(methodDelegate.Method);

            var result = testee.BuildSourceCode(request);

            result.Should().Be($@"var {expr};{Environment.NewLine}{expr};{Environment.NewLine}");
        }
    }
}
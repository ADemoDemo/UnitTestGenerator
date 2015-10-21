using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator.CodeGeneration.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq.Expressions;
using System.Reflection;

namespace UnitTestGenerator.CodeGeneration.Tests
{
    [TestClass]
    public class ExpressionBuilderTests
    {
        private IMockExpressionProvider mockExpressionProvider;
        private ExpressionBuilder testee;
        private IValueExpressionProvider valueExpressionProvider;
        private ITestMethodValueProvider testMethodValueProvider;

        [TestMethod]
        public void CreateArgumentExpressions_ParametersGiven_ShouldReturnExpressionEnumerable()
        {
            CreateArgumentExpressions_TestingExpicitCastArgument<ConstantExpression>(false);
        }

        [TestMethod]
        public void CreateArgumentExpressions_ParametersGivenWithRequiredExpicitCast_ShouldReturnExpressionEnumerable()
        {
            CreateArgumentExpressions_TestingExpicitCastArgument<UnaryExpression>(true);//convert expression
        }

        private void CreateArgumentExpressions_TestingExpicitCastArgument<TExpression>(bool explicitCast)
            where TExpression : Expression
        {
            Func<string, object, string> method = string.Format;

            var parameters = method.Method.GetParameters();
            var result = testee.CreateArgumentExpressions(parameters, explicitCast, parameters[0]);

            result.Should().NotBeNull();
            result.ElementAt(0).Should().BeAssignableTo<TExpression>();
            result.ElementAt(1).Should().BeAssignableTo<NewExpression>();
        }

        [TestMethod]
        public void CreateArgumentExpressions_NoParametersPassed_ShouldThrowArgumentException()
        {
            testee.Invoking(x => x.CreateArgumentExpressions(new ParameterInfo[0]))
                .ShouldThrow<ArgumentException>();
        }

        [TestMethod()]
        public void BuildInstanceInstantiationExpression_TypeGiven_ShouldReturnAssignExpresion()
        {
            var value = "abc";
            mockExpressionProvider.Stub(m => m.CreateMockExpression(null))
                .IgnoreArguments()
                .Return(Expression.Constant(value));

            ParameterExpression variableExpression;
            var resultExpression = testee.BuildInstanceCreationWithAssigmentExpression(typeof(string), out variableExpression);

            resultExpression.Should().NotBeNull();
            resultExpression.ToString().Should().Be($"(@string = \"{value}\")");
        }

        [TestMethod()]
        public void ExpressionToString_ExpressionGiven_ShouldReturnStringRepresentation()
        {
            var stringExpression = testee.ExpressionToString(Expression.Constant(5));

            stringExpression.Should().Be("5");
        }


        [TestMethod]
        public void CreateInstanceCreationExpression_TypeWithDefaultConstructorGiven_ShouldCreateSimpleExpression()
        {
            var expectedExpression = "new PublicClass()";

            var expression = testee.CreateInstanceCreationExpression(typeof(TestAssembly.PublicClass));
            var expressionAsString = ExpressionStringBuilder.ExpressionToString(expression);

            expressionAsString.Should().Be(expectedExpression);
        }

        [TestMethod]
        public void CreateInstanceCreationExpression_TypeWithParametrizedConstructorGiven_ShouldCreateExpressionUsingMockProvider()
        {
            var expectedExpression = Expression.Constant(5);
            mockExpressionProvider.Stub(m => m.CreateMockExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression);

            var result = testee.CreateInstanceCreationExpression(typeof(TestAssembly.ClassContructor));

            result.Should().Be(expectedExpression);
        }

        [TestMethod]
        public void CreateArgumentExpression_StringTypeGiven_ShouldCallValueProvider()
        {
            var expectedExpression = Expression.Constant(5);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression)
                .Repeat.Once();

            var result = testee.CreateArgumentExpression(typeof(string));

            result.Should().Be(expectedExpression);
            valueExpressionProvider.VerifyAllExpectations();
        }

        [TestMethod]
        public void CreateArgumentExpression_TypeRegisterInTestMethodValueProviderGiven_ShouldReturnRegisteredExpression()
        {
            var expectedExpression = Expression.Constant(5);
            var type = typeof(TestAssembly.PublicClass);

            Expression resultExpression;
            testMethodValueProvider.Stub(m => m.HasExpressionForArgument(type, out resultExpression))
                .OutRef(expectedExpression)
                .Return(true);

            var result = testee.CreateArgumentExpression(type);

            result.Should().Be(expectedExpression);
        }

        [TestMethod]
        public void CreateArgumentExpression_ArrayTypeGiven_ShouldReturnArrayCreationExpression()
        {
            var expectedExpression = Expression.Constant(5m);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression);

            var result = testee.CreateArgumentExpression(typeof(decimal[]));

            result.Should().BeAssignableTo<NewArrayExpression>();
        }

        [TestMethod]
        public void CreateArgumentExpression_GenericEnumerableTypeGiven_ShouldReturnArrayCreationExpression()
        {
            var expectedExpression = Expression.Constant(5m);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression);

            var result = testee.CreateArgumentExpression(typeof(IEnumerable<decimal>));

            result.Should().BeAssignableTo<NewArrayExpression>();
        }

        [TestMethod]
        public void CreateArgumentExpression_ValueTypeGiven_ShouldCallValueProvider()
        {
            var expectedExpression = Expression.Constant(5);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression)
                .Repeat.Once();

            var result = testee.CreateArgumentExpression(typeof(int));

            result.Should().Be(expectedExpression);
            valueExpressionProvider.VerifyAllExpectations();
        }

        [TestMethod]
        public void CreateArgumentExpression_CollectionInterfaceGiven_ShouldReturnCollectionCreationExpression()
        {
            var expectedExpression = Expression.Constant(5m);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression);

            var resultExpression = testee.CreateArgumentExpression(typeof(ICollection<decimal>));
            var expressionAsString = ExpressionStringBuilder.ExpressionToString(resultExpression);

            expressionAsString.Should().Be("new List<decimal>(new [] {5, 5})");
        }

        [TestMethod]
        public void CreateArgumentExpression_DictionaryInterfaceGiven_ShouldReturnDictionaryCreationExpression()
        {
            var decimalConstantExpression = Expression.Constant(5m);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(typeof(decimal)))
                .IgnoreArguments()
                .Return(decimalConstantExpression);
            var stringConstantExpression = Expression.Constant("abc");
            valueExpressionProvider.Stub(m => m.CreateValueExpression(typeof(string)))
                .IgnoreArguments()
                .Return(stringConstantExpression);

            var resultExpression = testee.CreateArgumentExpression(typeof(IDictionary<string, decimal>));
            var expressionAsString = ExpressionStringBuilder.ExpressionToString(resultExpression);

            expressionAsString.Should().Be(@"new Dictionary<string, decimal>()");
            //expressionAsString.Should().Be(@"new Dictionary<string, decimal>(new [] {{""abc"",5},{""abc"",5}})");
        }

        [TestMethod]
        public void CreateArgumentExpression_FuncTypeGiven_ShouldLambdaExpression()
        {
            var constantExpression = Expression.Constant("abc");
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(constantExpression);

            var resultExpression = testee.CreateArgumentExpression(typeof(Func<decimal, string>));
            //var expressionAsString = ExpressionStringBuilder.ExpressionToString(resultExpression);

            resultExpression.Should().BeAssignableTo<LambdaExpression>();
            var lambdaExpression = resultExpression as LambdaExpression;
            lambdaExpression.Body.Should().Be(constantExpression);
            //expressionAsString.Should().Be("new List<decimal>(new [] {5, 5})");
        }

        private delegate bool TryParseSignature(string s, out int result);

        [TestMethod]
        public void CreateArgumentExpression_TypeAsReferenceGiven_ShouldReferencedTypeCreationExpression()
        {
            TryParseSignature method = int.TryParse;
            var refType = method.Method.GetParameters().Last().ParameterType;
            var constantExpression = Expression.Constant(5);
            valueExpressionProvider.Stub(m => m.CreateValueExpression(null))
                .IgnoreArguments()
                .Return(constantExpression);

            var resultExpression = testee.CreateArgumentExpression(refType);

            refType.IsByRef.Should().Be(true);
            resultExpression.Should().NotBeNull();
            constantExpression.Value.Should().Be(5);
        }

        [TestMethod]
        public void CreateArgumentExpression_TypeWithDefaultConstructorGiven_ShouldReturnInstanceCreationExpression()
        {
            var resultExpression = testee.CreateArgumentExpression(typeof(TestAssembly.PublicClass));

            var expressionAsString = ExpressionStringBuilder.ExpressionToString(resultExpression);
            expressionAsString.Should().Be("new PublicClass()");
        }

        [TestMethod]
        public void CreateArgumentExpression_TypeWithParametrizedConstructorGiven_ShouldReturnInstanceCreationExpression()
        {
            var resultExpression = testee.CreateArgumentExpression(typeof(TestAssembly.ClassContructor));

            var expressionAsString = ExpressionStringBuilder.ExpressionToString(resultExpression);
            expressionAsString.Should().Be("new ClassContructor(new PublicClass())");
        }

        [TestMethod]
        public void CreateArgumentExpression_TypeWithProtectedDefaultConstructorGiven_ShouldReturnInstanceCreationExpression()
        {
            var expectedExpression = Expression.Constant(5);
            mockExpressionProvider.Stub(m => m.CreateMockExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression);

            var resultExpression = testee.CreateArgumentExpression(typeof(TestAssembly.ProtectedConstructor));

            resultExpression.Should().Be(expectedExpression);
        }

        [TestMethod]
        public void CreateArgumentExpression_GenericTypeGiven_ShouldThrowArgumentException()
        {

            testee.Invoking(x => x.CreateArgumentExpression(typeof(IList<>)))
                .ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void CreateArgumentExpression_UnknownCollectionTypeGiven_ShouldThrowArgumentException()
        {
            var expectedExpression = Expression.Constant(5);
            mockExpressionProvider.Stub(m => m.CreateMockExpression(null))
                .IgnoreArguments()
                .Return(expectedExpression);

            var resultExpression = testee.CreateArgumentExpression(typeof(TestAssembly.IMyList<decimal>));

            resultExpression.Should().Be(expectedExpression);
        }

        [TestMethod]
        public void CreateTestedInstanceExpression_TypeWithParametrizedConstructorGiven_ShouldInvokeCreateMockExpression()
        {
            var expected = Expression.Constant(77);
            mockExpressionProvider.Stub(x => x.CreateMockExpression(null))
                .IgnoreArguments()
                .Return(expected)
                .Repeat.Once();

            var expression = testee.CreateInstanceCreationExpression(typeof(TestAssembly.InterfaceConstructorParameter));

            expression.Should().BeSameAs(expected);
            mockExpressionProvider.VerifyAllExpectations();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testMethodValueProvider = MockRepository.GenerateMock<ITestMethodValueProvider>();
            mockExpressionProvider = MockRepository.GenerateMock<IMockExpressionProvider>();
            valueExpressionProvider = MockRepository.GenerateMock<IValueExpressionProvider>();

            testee = new ExpressionBuilder(testMethodValueProvider,
                mockExpressionProvider,
                valueExpressionProvider,
                MockRepository.GenerateMock<IIdentifierValidator>());

            //testee = new ExpressionBuilder(parameterExpressionBuilder,
            //    MockRepository.GenerateMock<IIdentifierValidator>());
        }
    }
}
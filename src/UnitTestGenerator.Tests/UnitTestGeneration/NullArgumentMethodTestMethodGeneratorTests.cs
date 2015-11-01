using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Rhino.Mocks;
using UnitTestGenerator.CodeGeneration.Generators;
using FluentAssertions;

namespace UnitTestGenerator.UnitTestGeneration.Tests
{
    [TestClass]
    public class NullArgumentMethodTestMethodGeneratorTests
    {
        const string expectedMethodName = "MethodName";
        const string expectedSourceCode = "var a = 0;";
        private INullArgumentMethodTestMethodSourceCodeGenerator testMethodSourceCodeGenerator;
        private NullArgumentMethodTestMethodGenerator testee;

        [TestMethod]
        public void GenerateTestMethods_DefaultConstructorTypeGiven()
        {
            var context = new TypeContext(typeof(TestAssembly.PublicClass), false);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().OnlyContain(x => x.Name == expectedMethodName);
            result.Should().OnlyContain(x => x.SourceCode == expectedSourceCode);
        }

        [TestMethod]
        public void GenerateTestMethods_TypeContainingProtectedMethodGiven()
        {
            var context = new TypeContext(typeof(TestAssembly.ProtectedConstructor), false);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GenerateTestMethods_TypeContainingInternalMethodGiven()
        {
            var context = new TypeContext(typeof(TestAssembly.InternalClass), true);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Should().OnlyContain(x => x.Name == expectedMethodName);
            result.Should().OnlyContain(x => x.SourceCode == expectedSourceCode);
        }

        [TestMethod]
        public void GenerateTestMethods_TypeWithOverloadsGiven()
        {
            var context = new TypeContext(typeof(TestAssembly.OverloadedMethods), true);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().OnlyContain(x => x.Name == expectedMethodName);
            result.Should().OnlyContain(x => x.SourceCode == expectedSourceCode);
        }

        [TestInitialize]
        public void Initialize()
        {
            testMethodSourceCodeGenerator = MockRepository.GenerateMock<INullArgumentMethodTestMethodSourceCodeGenerator>();
            testMethodSourceCodeGenerator.Stub(m => m.BuildMethodName(null))
                .IgnoreArguments()
                .Return(expectedMethodName);
            testMethodSourceCodeGenerator.Stub(m => m.BuildSourceCode(null))
                .IgnoreArguments()
                .Return(expectedSourceCode);

            testee = new NullArgumentMethodTestMethodGenerator(testMethodSourceCodeGenerator);
        }
    }
}
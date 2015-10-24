using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Rhino.Mocks;
using UnitTestGenerator.CodeGeneration.Generators;
using FluentAssertions;

namespace UnitTestGenerator.UnitTestGeneration.Tests
{
    [TestClass]
    public class NullArgumentConstructorTestMethodGeneratorTests
    {
        private NullArgumentConstructorTestMethodGenerator testee;
        const string expectedMethodName = "MethodName";
        const string expectedSourceCode = "var a = 0;";

        [TestMethod]
        public void GenerateTestMethods_SingleParametrizedConstructorTypeGiven_ShouldBuildSingleTestMethod()
        {
            var context = new TypeContext(typeof(TestAssembly.ClassContructor), false);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().ContainSingle();
            result.First().Name.Should().Be(expectedMethodName);
            result.First().SourceCode.Should().Be(expectedSourceCode);
        }

        [TestMethod]
        public void GenerateTestMethods_DefaultConstructorTypeGiven_ShouldNotReturnAnyTestMethod()
        {
            var context = new TypeContext(typeof(TestAssembly.PublicClass), false);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GenerateTestMethods_ProtectedConstructorTypeGiven_ShouldNotReturnAnyTestMethod()
        {
            var context = new TypeContext(typeof(TestAssembly.ProtectedConstructor), false);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GenerateTestMethods_InternalConstructorTypeGiven_ShouldNotReturnAnyTestMethod()
        {
            var context = new TypeContext(typeof(TestAssembly.InternalContructorParameter), false);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GenerateTestMethods_InternalConstructorTypeWithInternalsVisibleGiven_ShouldNotReturnAnyTestMethod()
        {
            var context = new TypeContext(typeof(TestAssembly.InternalContructorParameter), true);
            var result = testee.GenerateTestMethods(context);

            result.Should().NotBeNull();
            result.Should().ContainSingle();
            result.First().Name.Should().Be(expectedMethodName);
            result.First().SourceCode.Should().Be(expectedSourceCode);
        }


        [TestInitialize]
        public void Initialize()
        {
            var testMethodSourceCodeGenerator = MockRepository.GenerateMock<INullArgumentConstructorTestMethodSourceCodeGenerator>();
            testMethodSourceCodeGenerator.Stub(m => m.BuildMethodName(null))
                .IgnoreArguments()
                .Return(expectedMethodName);
            testMethodSourceCodeGenerator.Stub(m => m.BuildSourceCode(null))
                .IgnoreArguments()
                .Return(expectedSourceCode);
            testee = new NullArgumentConstructorTestMethodGenerator(testMethodSourceCodeGenerator);
        }
    }
}
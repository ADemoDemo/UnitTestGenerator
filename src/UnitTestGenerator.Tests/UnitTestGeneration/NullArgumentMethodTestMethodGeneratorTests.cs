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
        [TestMethod]
        public void GenerateTestMethods_DefaultConstructorTypeGiven()
        {
            const string expectedMethodName = "MethodName";
            const string expectedSourceCode = "var a = 0;";
            var testMethodSourceCodeGenerator = MockRepository.GenerateMock<INullArgumentMethodTestMethodSourceCodeGenerator>();
            testMethodSourceCodeGenerator.Stub(m => m.BuildMethodName(null))
                .IgnoreArguments()
                .Return(expectedMethodName);
            testMethodSourceCodeGenerator.Stub(m => m.BuildSourceCode(null))
                .IgnoreArguments()
                .Return(expectedSourceCode);
            var testee = new NullArgumentMethodTestMethodGenerator(testMethodSourceCodeGenerator);

            var result = testee.GenerateTestMethods(typeof(TestAssembly.PublicClass));

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().OnlyContain(x => x.Name == expectedMethodName);
            result.Should().OnlyContain(x => x.SourceCode == expectedSourceCode);
        }
    }
}
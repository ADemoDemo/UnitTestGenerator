using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator.DynamicProxy;
using FluentAssertions;

namespace UnitTestGenerator.Tests
{
    public partial class TestMethodTests
    {
        [TestMethod]
        public void TestMethodConstructor_NullExceptionTypeGiven_Succeeds()
        {
            var stringMethod = typeof(string).GetMethods().First();
            var name = Value.Create<string>();
            var sourceCode = Value.Create<string>();

            var testee = new TestMethod(stringMethod, name, sourceCode, null);

            testee.Name.Should().Be(name);
            testee.SourceCode.Should().Be(sourceCode);
            testee.ShouldThrowException.Should().BeNull();
            testee.TestedMember.Should().Be(stringMethod);
        }

        [TestMethod]
        public void TestMethodConstructor_NonExceptionTypeGiven_ShouldThrowArgumentException()
        {
            ((Action)(() => Instantiate(typeof(string))))
                .ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestMethodConstructor_ExceptionTypeGiven_ShouldCreateInstance()
        {
            var exceptionType = typeof(ArgumentNullException);
            var testee = Instantiate(exceptionType);

            testee.ShouldThrowException.Should().Be(exceptionType);
        }

        private static TestMethod Instantiate(Type exceptionType)
        {
            var stringMethod = typeof(string).GetMethods().First();
            var name = Value.Create<string>();
            var sourceCode = Value.Create<string>();
//            var exceptionType = typeof(ArgumentNullException);

            return new TestMethod(stringMethod, name, sourceCode, exceptionType);
        }
    }
}
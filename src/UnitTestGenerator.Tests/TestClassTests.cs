using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace UnitTestGenerator.Tests
{
    [TestClass]
    public class TestClassTests
    {
        private TestMethod[] methods;

        [TestMethod()]
        public void TestClassConstructor_Always_Succeeds()
        {
            var type = typeof(string);

            var instance = new TestClass(type, methods);

            instance.Should().NotBeNull();
            instance.TestedType.Should().Be(type);
            instance.Methods.Should().BeEquivalentTo(methods);
        }

        [TestInitialize]
        public void Initialize()
        {
            methods = new[] { new TestMethod(typeof(string).GetMethods().First(), "name", "code") };
        }
    }
}
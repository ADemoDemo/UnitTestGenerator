using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;
using Rhino.Mocks;
using UnitTestGenerator.CodeGeneration;
using FluentAssertions;
using UnitTestGenerator.UnitTestGeneration;
using UnitTestGenerator.Integration;

namespace UnitTestGenerator.Tests
{
    [TestClass()]
    public class UnitTestBuilderExtensionsTests
    {
        //[TestMethod()]
        //public void UseBuiltinGeneratorsTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void Scan_Always_ShouldReturnTestClassBuilder()
        {
            var mockExpressionProvider = MockRepository.GenerateMock<IMockExpressionProvider>();
            var valueExpressionProvider = MockRepository.GenerateMock<IValueExpressionProvider>();
            var testMethodGenerator = MockRepository.GenerateMock<ITestMethodGenerator>();

            var builder = typeof(PublicClass).Assembly
               .Scan(typeof(UnitTestBuilderExtensionsTests).Assembly.GetName().Name,
                   mockExpressionProvider,
                   valueExpressionProvider,
                   configurator => configurator.AddGenerator(testMethodGenerator)
                        .ParameterTypeMapping(new Dictionary<Type, string> { { typeof(string), "stringField" } })
                        .Excluding(typeof(string))
                   );

            builder.Should().NotBeNull();
        }

        //[TestMethod()]
        //public void ScanTest1()
        //{
        //    Assert.Fail();
        //}
    }
}
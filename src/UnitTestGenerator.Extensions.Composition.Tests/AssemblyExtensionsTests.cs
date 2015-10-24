using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Rhino.Mocks;
using UnitTestGenerator.CodeGeneration;
using TestAssembly;
using FluentAssertions;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGenerator.Extensions.Composition.Tests
{
    [TestClass()]
    public class AssemblyExtensionsTests
    {
        [TestMethod()]
        public void ComposeTestClassBuilder_Always_ShouldReturnTestClassBuilder()
        {
            var mockExpressionProvider = MockRepository.GenerateMock<IMockExpressionProvider>();
            var valueExpressionProvider = MockRepository.GenerateMock<IValueExpressionProvider>();
            var testMethodGenerator = MockRepository.GenerateMock<ITestMethodGenerator>();

            var builder = typeof(PublicClass).Assembly
               .ComposeTestClassBuilder(typeof(AssemblyExtensionsTests).Assembly.GetName().Name,
                   mockExpressionProvider,
                   valueExpressionProvider,
                   configurator => configurator.AddGenerator(testMethodGenerator)
                        .ParameterTypeMapping(new Dictionary<Type, string> { { typeof(string), "stringField" } })
                        .Excluding(typeof(string))
                   );

            builder.Should().NotBeNull();
        }
    }
}
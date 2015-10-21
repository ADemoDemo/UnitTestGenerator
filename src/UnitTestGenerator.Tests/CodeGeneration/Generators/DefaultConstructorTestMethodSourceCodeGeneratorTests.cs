using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator.CodeGeneration.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Mocks;
using TestAssembly;
using FluentAssertions;

namespace UnitTestGenerator.CodeGeneration.Generators.Tests
{
    //[TestClass()]
    //public class DefaultConstructorTestMethodSourceCodeGeneratorTests
    //{
    //    [TestMethod]
    //    public void Constructor_Always_Succeeds()
    //    {
    //        var parameterExpressionBuilder = MockRepository.GenerateMock<IParameterExpressionBuilder>();
    //        var generator = new DefaultConstructorTestMethodSourceCodeGenerator(parameterExpressionBuilder);
    //    }

    //    [TestMethod]
    //    public void BuildMethodName()
    //    {
    //        var parameterExpressionBuilder = MockRepository.GenerateMock<IParameterExpressionBuilder>();
    //        var generator = new DefaultConstructorTestMethodSourceCodeGenerator(parameterExpressionBuilder);
    //        var type = typeof(PublicClass);
    //        var request = new ConstructorSourceCodeGenerationRequest(type.GetConstructors().First(), false, false, null);

    //        var result = generator.BuildMethodName(request);

    //        result.Should().EndWith("_Constructor_Default");
    //    }

    //    [TestMethod]
    //    public void BuildSourceCode()
    //    {
    //        var parameterExpressionBuilder = MockRepository.GenerateMock<IParameterExpressionBuilder>();
    //        var generator = new DefaultConstructorTestMethodSourceCodeGenerator(parameterExpressionBuilder);
    //        var type = typeof(PublicClass);
    //        var request = new ConstructorSourceCodeGenerationRequest(type.GetConstructors().First(), false, false, null);

    //        var result = generator.BuildSourceCode(request);

    //     //   result.Should().EndWith("_Constructor_Default");
    //    }
    //}
}
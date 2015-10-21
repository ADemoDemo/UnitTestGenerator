using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator.UnitTestGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;
using System.Reflection;

namespace UnitTestGenerator.UnitTestGeneration.Tests
{
    //[TestClass()]
    //public class AbstractMethodTestGeneratorTests
    //{
    //    [TestMethod()]
    //    public void GenerateUnitTestsTest()
    //    {
    //        var generator = new DummyAbstractMethodTestGenerator(GetMock<ITestMethodValueProvider>(),
    //            GetMock<IIdentifierValidator>(),
    //            //GetMock<ITestMethodNameBuilder>(),
    //            GetMock<IParameterExpressionBuilder>());
    //        //var mock = new Mock<AbstractMethodTestGenerator>();
    //        //mock.Setup(x => x.)

    //        var tests = generator.GenerateUnitTests(typeof(OverloadedMethods));
    //    }

    //    [TestMethod()]
    //    public void GenerateUnitTestsTest_TestedTypeMember()
    //    {
    //        var generator = new DummyAbstractMethodTestGenerator(GetMock<ITestMethodValueProvider>(),
    //            GetMock<IIdentifierValidator>(),
    //            //GetMock<ITestMethodNameBuilder>(),
    //            GetMock<IParameterExpressionBuilder>());
    //        //var mock = new Mock<AbstractMethodTestGenerator>();
    //        //mock.Setup(x => x.)

    //        var tests = generator.GenerateUnitTests(typeof(TestedTypeMember));
    //    }

    //    private T GetMock<T>()
    //        where T : class
    //    {
    //        return new Mock<T>().Object;
    //    }

    //    private class DummyAbstractMethodTestGenerator : AbstractMethodTestGenerator
    //    {
    //        public DummyAbstractMethodTestGenerator(ITestMethodValueProvider testMethodValueProvider,
    //            IIdentifierValidator identifierValidator,
    //            IParameterExpressionBuilder parameterExpressionBuilder)
    //            : base(testMethodValueProvider, identifierValidator, parameterExpressionBuilder)
    //        {
    //        }

    //        protected override IEnumerable<MethodUnitTest> GetTestForMethod(TestedMethod testedMethod)
    //        {
    //            return new MethodUnitTest[0];
    //        }
    //    }
    //}
}
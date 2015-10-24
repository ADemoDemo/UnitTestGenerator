using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestAssembly;
using UnitTestGenerator.ExpressionProviders;

namespace UnitTestGenerator.Extensions.Composition.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        [TestCategory("IntergrationTests")]
        public void TestAssemblyScan()
        {
            var tests = typeof(PublicClass).Assembly
                .ComposeTestClassBuilder(typeof(IntegrationTests).Assembly.GetName().Name,
                    new CastleMockProvider(),
                    new ValueExpressionProvider())
                .BuildTestClasses();
        }


        [TestMethod]
        [TestCategory("IntergrationTests")]
        public void UnitTestGeneratorScan()
        {
            var tests = typeof(TestClassBuilder).Assembly
                .ComposeTestClassBuilder(typeof(IntegrationTests).Assembly.GetName().Name,
                    new CastleMockProvider(),
                    new ValueExpressionProvider(),
                    configure => configure
                        .IncludeBuiltinGenerators()
                        .WithDefaultValues(new[] {
                         (Expression<Func<MemberInfo>>)(() => ((Func<string, string>)string.Copy).Method)
                        }))
                .BuildTestClasses();
        }

        [TestMethod]
        [TestCategory("IntergrationTests")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnitTestGeneratorScan_ContainerNotSetup()
        {
            var tests = typeof(TestClassBuilder).Assembly
                .ComposeTestClassBuilder(typeof(IntegrationTests).Assembly.GetName().Name,
                    x => { },
                    configure => configure
                        .IncludeBuiltinGenerators()
                        .WithDefaultValues(new[] {
                         (Expression<Func<MemberInfo>>)(() => ((Func<string, string>)string.Copy).Method)
                        }))
                .BuildTestClasses();
        }
    }
}

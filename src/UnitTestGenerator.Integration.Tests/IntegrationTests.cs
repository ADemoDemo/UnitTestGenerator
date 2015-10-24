using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using UnitTestGenerator.DynamicProxy;
using UnitTestGenerator.Integration;
using UnitTestGenerator.ExpressionProviders;

namespace UnitTestGenerator.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        [TestCategory("IntergrationTests")]
        public void TestAssemblyScan()
        {
            var tests = typeof(PublicClass).Assembly
                .Scan(typeof(IntegrationTests).Assembly.GetName().Name,
                    new CastleMockProvider(),
                    new AutoFixtureValueExpressionProvider())
                .BuildTestClasses();
        }


        [TestMethod]
        [TestCategory("IntergrationTests")]
        public void UnitTestGeneratorScan()
        {
            var tests = typeof(TestClassBuilder).Assembly
                .Scan(typeof(IntegrationTests).Assembly.GetName().Name,
                    new CastleMockProvider(),
                    new AutoFixtureValueExpressionProvider(),
                    configure => configure
                        .UseBuiltinGenerators()
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
                .Scan(typeof(IntegrationTests).Assembly.GetName().Name,
                    x => { },
                    configure => configure
                        .UseBuiltinGenerators()
                        .WithDefaultValues(new[] {
                         (Expression<Func<MemberInfo>>)(() => ((Func<string, string>)string.Copy).Method)
                        }))
                .BuildTestClasses();
        }
    }
}

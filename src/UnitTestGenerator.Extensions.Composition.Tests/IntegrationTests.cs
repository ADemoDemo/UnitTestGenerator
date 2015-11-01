using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TestAssembly;
using UnitTestGenerator.ExpressionProviders;
using FluentAssertions;

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

            tests.Should().OnlyContain(cls => cls.Methods.Select(x => x.Name).Distinct().Count() == cls.Methods.Count(), "method names are not distinct.");
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
    }
}

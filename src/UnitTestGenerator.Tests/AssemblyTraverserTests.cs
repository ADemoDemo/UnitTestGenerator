using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace UnitTestGenerator.Tests
{

    public partial class AssemblyTraverserTests
    {
        private AssemblyTraverser testee;

        [TestInitialize]
        public void Initialize()
        {
            testee = AssemblyTraverser.Create(typeof(AssemblyTraverser).Assembly, typeof(AssemblyTraverserTests).Assembly.GetName().Name);
        }

        [TestMethod]
        public void InternalsVisible_FriendlyAssemblyGiven_ShouldReturnTrue()
        {
            var assembly = typeof(TestAssembly.InternalClass).Assembly;
            string friendlyToAssembly = "UnitTestGenerator.Tests";

            var result = AssemblyTraverser.InternalsVisible(assembly, friendlyToAssembly);

            result.Should().Be(true);
        }

        [TestMethod]
        public void InternalsVisible_UnfriendlyAssemblyGiven_ShouldReturnFalse()
        {
            var assembly = typeof(string).Assembly;
            string notFriendlyToAssembly = "UnitTestGenerator.Tests";

            var result = AssemblyTraverser.InternalsVisible(assembly, notFriendlyToAssembly);

            result.Should().Be(false);
        }

        [TestMethod]
        public void GetTypes_UnfriendlyAssemblyGiven_ShouldNotReturnInternalTypes()
        {
            var assembly = typeof(TestAssembly.AbstractClass).Assembly;

            testee = AssemblyTraverser.Create(assembly, "Fake");
            var result = testee.GetTypes();

            result.Should().Contain(typeof(TestAssembly.PublicClass));
            result.Should().NotContain(typeof(TestAssembly.InternalClass));
            result.Should().NotContain(typeof(TestAssembly.AbstractClass));
            result.Should().NotContain(typeof(TestAssembly.NestedClasses.NestedPublicClass));
            result.Should().NotContain(typeof(TestAssembly.GenericClass<>));
        }

        [TestMethod]
        public void GetTypes_FriendlyAssemblyGiven_ShouldReturnInternalTypes()
        {
            var assembly = typeof(TestAssembly.AbstractClass).Assembly;

            testee = AssemblyTraverser.Create(assembly, "UnitTestGenerator.Tests");
            var result = testee.GetTypes();

            result.Should().Contain(typeof(TestAssembly.InternalClass));
        }

      

        [TestMethod]
        public void Exclude_NullTypeGiven_ShouldThrowArgumentNullException()
        {
            IAssemblyTraverserConfigurator configurator = testee;
            configurator.Invoking(x => x.Exclude(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Exclude_TypeGiven_ShouldBeExcluded()
        {
            var assembly = typeof(TestAssembly.AbstractClass).Assembly;
            var typeToExclude = typeof(TestAssembly.ClassContructor);

            testee = AssemblyTraverser.Create(assembly, "UnitTestGenerator.Tests");
            IAssemblyTraverserConfigurator configurator = testee;
            configurator.Exclude(new[] { typeToExclude });
            var result = testee.GetTypes();

            result.Should().NotContain(typeToExclude);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
//using Moq;
using UnitTestGenerator.UnitTestGeneration;
using Rhino.Mocks;
//using Rhino.Mocks;
using System.Linq;
namespace UnitTestGenerator.Tests
{

    public partial class TestClassBuilderTests
    {
        //private Mock<IAssemblyTraverser> assemblyTraverserMock;
        //private Mock<ITestMethodGenerator> testMethodGeneratorMock;
        private ITestMethodGenerator[] generators;
        private IAssemblyTraverser assemblyTraverser;
        private ITestMethodGenerator testMethodGenerator;

        [TestMethod()]
        public void TestClassBuilderConstructor_Always_Succeeds()
        {
            var instance = new TestClassBuilder(assemblyTraverser, generators);

            instance.Should().NotBeNull();
        }

        [TestMethod()]
        public void BuildTestClasses_TraverserWithAtLeastOnePublicTypeGiven_ShouldReturnTestClass()
        {
            //Arrange
            var testMethod = new TestMethod(typeof(string).GetMethods().First(), "name", "code");

            assemblyTraverser.Expect(x => x.GetTypes())
                .Return(new[] { typeof(string) })
                .Repeat.Once();

            testMethodGenerator.Expect(m => m.GenerateTestMethods(null))
                .IgnoreArguments()
                .Return(new[] { testMethod });

            var instance = new TestClassBuilder(assemblyTraverser, generators);

            //Act
            var classes = instance.BuildTestClasses();

            //Assert
            assemblyTraverser.VerifyAllExpectations();
            classes.Should().OnlyContain(x => x.Methods.All(m => m == testMethod));
        }

        [TestInitialize]
        public void Initialize()
        {
            //assemblyTraverserMock = new Mock<IAssemblyTraverser>();
            //testMethodGeneratorMock = new Mock<ITestMethodGenerator>();
            assemblyTraverser = MockRepository.GenerateStub<IAssemblyTraverser>();
            testMethodGenerator = MockRepository.GenerateStub<ITestMethodGenerator>();

            generators = new[] { testMethodGenerator };
        }
    }
}
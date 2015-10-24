using System;
using System.Reflection;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGeneration.Extensions.Composition
{
    public static class UnitTestBuilderExtensions
    {
        public static ITestBuilderConfigurator UseBuiltinGenerators(this ITestBuilderConfigurator configurator)
        {
            var testGeneratorConfigurator = configurator as TestGeneratorConfigurator;

            configurator.AddGenerator<NullArgumentMethodTestMethodGenerator>()
                .AddGenerator<NullArgumentConstructorTestMethodGenerator>();

            return configurator;
        }

        public static ITestClassBuilder Scan(this Assembly assembly, string callingAssemblyName, IMockExpressionProvider mockProvider, IValueExpressionProvider valueExpressionProvider)
        {
            return Scan(assembly, callingAssemblyName, mockProvider, valueExpressionProvider, null);
        }

        public static ITestClassBuilder Scan(this Assembly assembly,
            string callingAssemblyName,
            IMockExpressionProvider mockProvider,
            IValueExpressionProvider valueExpressionProvider,
            Action<ITestBuilderConfigurator> configure)
        {
            return Scan(assembly, callingAssemblyName, container =>
            {
                container.Register(mockProvider);
                container.Register(valueExpressionProvider);
            }, configure);
        }

        public static ITestClassBuilder Scan(this Assembly assembly,
            string callingAssemblyName,
            Action<IContainer> typeRegistration,
            Action<ITestBuilderConfigurator> configure)
        {
            var assemblyTraverser = AssemblyTraverser.Create(assembly, callingAssemblyName);
            var generatorRegistrationManager = new GeneratorRegistrationManager();
            var testGeneratorConfigurator = new TestGeneratorConfigurator(assemblyTraverser, generatorRegistrationManager);
            var setup = new TestMethodGeneratorComposition(assemblyTraverser, testGeneratorConfigurator, generatorRegistrationManager);
            var testGenerators = setup.GetGenerators(typeRegistration, configure);
            return new TestClassBuilder(assemblyTraverser, testGenerators);
        }
    }
}

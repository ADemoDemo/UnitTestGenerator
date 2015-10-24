using System;
using System.Reflection;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.ExpressionProviders;

namespace UnitTestGenerator.Extensions.Composition
{
    /// <summary>
    /// Extension methods for composing UnitTestBuilder.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Compose an instance of TestClassBuilder class.
        /// </summary>
        /// <param name="assembly">The assembly which types should be used.</param>
        /// <param name="callingAssemblyName">Name of calling assembly, which is set in <paramref name="assembly"/> in attribute InternalsVisibleTo.</param>
        /// <param name="mockProvider">An instance of <see cref="IMockExpressionProvider"/> for generating expressions of values of complex type.</param>
        /// <param name="valueExpressionProvider">An instance of <see cref="IValueExpressionProvider"/> for generating expressions of values of primitive types.</param>
        /// <returns>The instance of <see cref="ITestClassBuilder"/>.</returns>
        public static ITestClassBuilder ComposeTestClassBuilder(this Assembly assembly, 
            string callingAssemblyName, 
            IMockExpressionProvider mockProvider = null, 
            IValueExpressionProvider valueExpressionProvider = null)
        {
            return ComposeTestClassBuilder(assembly, callingAssemblyName, mockProvider, valueExpressionProvider, null);
        }

        /// <summary>
        /// Compose an instance of TestClassBuilder class.
        /// </summary>
        /// <param name="assembly">The assembly which types should be used.</param>
        /// <param name="callingAssemblyName">Name of calling assembly, which is set in <paramref name="assembly"/> in attribute InternalsVisibleTo.</param>
        /// <param name="mockProvider">An instance of <see cref="IMockExpressionProvider"/> for generating expressions of values of complex type.</param>
        /// <param name="valueExpressionProvider">An instance of <see cref="IValueExpressionProvider"/> for generating expressions of values of primitive types.</param>
        /// <param name="configure">Action where the configuration is set up.</param>
        /// <returns>The instance of <see cref="ITestClassBuilder"/>.</returns>
        public static ITestClassBuilder ComposeTestClassBuilder(this Assembly assembly,
            string callingAssemblyName,
            IMockExpressionProvider mockProvider = null,
            IValueExpressionProvider valueExpressionProvider = null,
            Action<ITestMethodGeneratorConfigurator> configure = null)
        {
            return ComposeTestClassBuilder(assembly, callingAssemblyName, container =>
            {
                container.Register(mockProvider ?? new CastleMockProvider());
                container.Register(valueExpressionProvider ?? new ValueExpressionProvider());
            }, configure);
        }

        /// <summary>
        /// Compose an instance of TestClassBuilder class.
        /// </summary>
        /// <param name="assembly">The assembly which types should be used.</param>
        /// <param name="callingAssemblyName">Name of calling assembly, which is set in <paramref name="assembly"/> in attribute InternalsVisibleTo.</param>
        /// <param name="typeRegistration">The action where the type registration is set up.</param>
        /// <param name="configure">The action where the configuration is set up.</param>
        /// <returns>The instance of <see cref="ITestClassBuilder"/>.</returns>
        public static ITestClassBuilder ComposeTestClassBuilder(this Assembly assembly,
            string callingAssemblyName,
            Action<IContainer> typeRegistration,
            Action<ITestMethodGeneratorConfigurator> configure = null)
        {
            var assemblyTraverser = AssemblyTraverser.Create(assembly, callingAssemblyName);
            var generatorRegistrationManager = new GeneratorRegistrationManager();
            var testGeneratorConfigurator = new TestMethodGeneratorConfigurator(assemblyTraverser, generatorRegistrationManager);
            var setup = new TestMethodGeneratorComposition(assemblyTraverser, testGeneratorConfigurator, generatorRegistrationManager);
            var testGenerators = setup.GetGenerators(typeRegistration, configure);
            return new TestClassBuilder(assemblyTraverser, testGenerators);
        }
    }
}

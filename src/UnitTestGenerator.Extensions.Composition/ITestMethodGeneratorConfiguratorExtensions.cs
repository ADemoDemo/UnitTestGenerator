using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGenerator.Extensions.Composition
{
    /// <summary>
    /// Extensions methods for TestMethodGeneratorConfigurator.
    /// </summary>
    public static class ITestMethodGeneratorConfiguratorExtensions
    {
        /// <summary>
        /// Includes to configuration two of the default TestMethodGenerator(<see cref="NullArgumentMethodTestMethodGenerator"/>, <see cref="NullArgumentConstructorTestMethodGenerator"/>).
        /// </summary>
        /// <param name="configurator">The instance of configuration.</param>
        /// <returns>The instance of configuration.</returns>
        public static ITestMethodGeneratorConfigurator IncludeBuiltinGenerators(this ITestMethodGeneratorConfigurator configurator)
        {
            var testGeneratorConfigurator = configurator as TestMethodGeneratorConfigurator;

            configurator.AddGenerator<NullArgumentMethodTestMethodGenerator>()
                .AddGenerator<NullArgumentConstructorTestMethodGenerator>();

            return configurator;
        }

        /// <summary>
        /// Includes <see cref="NullArgumentMethodTestMethodGenerator"/> to configuration.
        /// </summary>
        /// <param name="configurator">The instance of configuration.</param>
        /// <returns>The instance of configuration.</returns>
        public static ITestMethodGeneratorConfigurator IncludeNullArgumentMethodTestMethodGenerator(this ITestMethodGeneratorConfigurator configurator)
        {
            configurator.AddGenerator<NullArgumentMethodTestMethodGenerator>();
            return configurator;
        }

        /// <summary>
        /// Includes <see cref="NullArgumentConstructorTestMethodGenerator"/> to configuration.
        /// </summary>
        /// <param name="configurator">The instance of configuration.</param>
        /// <returns>The instance of configuration.</returns>
        public static ITestMethodGeneratorConfigurator IncludeNullArgumentConstructorTestMethodGenerator(this ITestMethodGeneratorConfigurator configurator)
        {
            configurator.AddGenerator<NullArgumentConstructorTestMethodGenerator>();
            return configurator;
        }
    }
}

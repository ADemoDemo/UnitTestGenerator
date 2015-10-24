using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGeneration.Extensions.Composition
{
    /// <summary>
    /// Provides chain methods for configuring TestMethodGenerators.
    /// </summary>
    public interface ITestMethodGeneratorConfigurator
    {
        /// <summary>
        /// Excludes types from result.
        /// </summary>
        /// <param name="typesToExclude">Types which should be excluded from result.</param>
        /// <returns>The instance of configurator</returns>
        ITestMethodGeneratorConfigurator Excluding(params Type[] typesToExclude);

        /// <summary>
        /// Defines tested classes which should not be instantiated, instead the passed string should be used as variable name of defined class.
        /// Recommended for types with private constructors or types that can not be built with public accessor.
        /// </summary>
        /// <param name="parametersForType">The dictionary mapping classes to variable names.</param>
        /// <returns>The instance of configurator</returns>
        ITestMethodGeneratorConfigurator ParameterTypeMapping(IDictionary<Type, string> parametersForType);

        /// <summary>
        /// Defines expressions which should be used as arguments to tested methods, instead of automatically generated expressions. 
        /// The type is extracted from the expression return type. Recommended for types with private constructors or types that can not be built with public accessor.
        /// </summary>
        /// <param name="defaultValues">The enumeration of expressions.</param>
        /// <returns>The instance of configurator</returns>
        ITestMethodGeneratorConfigurator WithDefaultValues(IEnumerable<LambdaExpression> defaultValues);

        /// <summary>
        /// Add an instance of <see cref="ITestMethodGenerator"/> which will be passed to TextClassBuilder.
        /// </summary>
        /// <param name="testMethodGenerator">The instance of <see cref="ITestMethodGenerator"/></param>
        /// <returns>The instance of configurator</returns>
        ITestMethodGeneratorConfigurator AddGenerator(ITestMethodGenerator testMethodGenerator);

        /// <summary>
        /// Add an Type of <see cref="ITestMethodGenerator"/> which will composed and passed to TextClassBuilder.
        /// </summary>
        /// <typeparam name="TGenerator">The type which implements from ITestMethodGenerator.</typeparam>
        /// <returns>The instance of configurator</returns>
        ITestMethodGeneratorConfigurator AddGenerator<TGenerator>() where TGenerator : ITestMethodGenerator;

        /// <summary>
        /// Add an callback which returns an instance of type <see cref="ITestMethodGenerator"/> which will passed to TextClassBuilder.
        /// </summary>
        /// <param name="instanceProducer">The callback returning the instance of type <see cref="ITestMethodGenerator"/>.</param>
        /// <returns>The instance of configurator</returns>
        ITestMethodGeneratorConfigurator AddGenerator(Func<IServiceProvider, ITestMethodGenerator> instanceProducer);
    }
}

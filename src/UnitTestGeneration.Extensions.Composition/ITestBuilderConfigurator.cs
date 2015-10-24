using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGeneration.Extensions.Composition
{
    public interface ITestBuilderConfigurator
    {
        ITestBuilderConfigurator Excluding(params Type[] typesToExclude);
        ITestBuilderConfigurator ParameterTypeMapping(IDictionary<Type, string> parametersForType);
        ITestBuilderConfigurator WithDefaultValues(IEnumerable<LambdaExpression> defaultValues);
        ITestBuilderConfigurator AddGenerator(ITestMethodGenerator testMethodGenerator);
        ITestBuilderConfigurator AddGenerator<TGenerator>() where TGenerator : ITestMethodGenerator;
        ITestBuilderConfigurator AddGenerator(Func<IServiceProvider, ITestMethodGenerator> instanceProducer);
    }
}

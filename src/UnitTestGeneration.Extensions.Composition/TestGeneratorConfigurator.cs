using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGeneration.Extensions.Composition
{
    class TestGeneratorConfigurator : ITestBuilderConfigurator
    {
        private ITestMethodValueProvider defaultValueForTypeMapper;
        private AssemblyTraverser traverser;
        private IDictionary<Type, string> parametersForType = new Dictionary<Type, string>();
        private Type[] ignoredConstructorTypes = new Type[0];
        private IEnumerable<LambdaExpression> defaultValues = new LambdaExpression[0];
        private readonly GeneratorRegistrationManager generatorManager;

        public TestGeneratorConfigurator(AssemblyTraverser traverser, GeneratorRegistrationManager generatorManager)
        {
            this.traverser = traverser;
            this.generatorManager = generatorManager;
        }

        public IEnumerable<Type> IgnoredConstructorTypesForRandomArgumentConstructors
        {
            get { return ignoredConstructorTypes; }
        }

        public ITestBuilderConfigurator Excluding(params Type[] excluding)
        {
            var asmConfig = (IAssemblyTraverserConfigurator)traverser;
            asmConfig.Exclude(excluding);
            return this;
        }

        public ITestBuilderConfigurator ParameterTypeMapping(IDictionary<Type, string> parametersForType)
        {
            this.parametersForType = parametersForType;
            return this;
        }

        public ITestBuilderConfigurator WithDefaultValues(IEnumerable<LambdaExpression> defaultValues)
        {
            this.defaultValues = defaultValues;
            return this;
        }

        internal ITestMethodValueProvider GetTestMethodValueProvider()
        {
            if (defaultValueForTypeMapper == null)
            {
                defaultValueForTypeMapper = new DefaultValueForTypeMapper(parametersForType, defaultValues);
            }
            return defaultValueForTypeMapper;
        }

        public ITestBuilderConfigurator AddGenerator(ITestMethodGenerator testMethodGenerator)
        {
            generatorManager.AddGenerator(testMethodGenerator);
            return this;
        }

        public ITestBuilderConfigurator AddGenerator<TGenerator>() where TGenerator : ITestMethodGenerator
        {
            generatorManager.AddGenerator<TGenerator>();
            return this;
        }

        public ITestBuilderConfigurator AddGenerator(Func<IServiceProvider, ITestMethodGenerator> instanceProducer)
        {
            generatorManager.AddGenerator(instanceProducer);
            return this;
        }
    }
}

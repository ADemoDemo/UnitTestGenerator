using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGeneration.Extensions.Composition
{
    class TestMethodGeneratorConfigurator : ITestMethodGeneratorConfigurator
    {
        private ITestMethodValueProvider defaultValueForTypeMapper;
        private AssemblyTraverser traverser;
        private IDictionary<Type, string> parametersForType = new Dictionary<Type, string>();
        private Type[] ignoredConstructorTypes = new Type[0];
        private IEnumerable<LambdaExpression> defaultValues = new LambdaExpression[0];
        private readonly GeneratorRegistrationManager generatorManager;

        public TestMethodGeneratorConfigurator(AssemblyTraverser traverser, GeneratorRegistrationManager generatorManager)
        {
            this.traverser = traverser;
            this.generatorManager = generatorManager;
        }

        public IEnumerable<Type> IgnoredConstructorTypesForRandomArgumentConstructors
        {
            get { return ignoredConstructorTypes; }
        }

        public ITestMethodGeneratorConfigurator Excluding(params Type[] excluding)
        {
            var asmConfig = (IAssemblyTraverserConfigurator)traverser;
            asmConfig.Exclude(excluding);
            return this;
        }

        public ITestMethodGeneratorConfigurator ParameterTypeMapping(IDictionary<Type, string> parametersForType)
        {
            this.parametersForType = parametersForType;
            return this;
        }

        public ITestMethodGeneratorConfigurator WithDefaultValues(IEnumerable<LambdaExpression> defaultValues)
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

        public ITestMethodGeneratorConfigurator AddGenerator(ITestMethodGenerator testMethodGenerator)
        {
            generatorManager.AddGenerator(testMethodGenerator);
            return this;
        }

        public ITestMethodGeneratorConfigurator AddGenerator<TGenerator>() where TGenerator : ITestMethodGenerator
        {
            generatorManager.AddGenerator<TGenerator>();
            return this;
        }

        public ITestMethodGeneratorConfigurator AddGenerator(Func<IServiceProvider, ITestMethodGenerator> instanceProducer)
        {
            generatorManager.AddGenerator(instanceProducer);
            return this;
        }
    }
}

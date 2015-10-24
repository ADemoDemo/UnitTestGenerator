using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.CodeGeneration.Generators;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGeneration.Extensions.Composition
{
    class TestMethodGeneratorComposition
    {
        readonly GeneratorRegistrationManager generatorRegistrationManager = new GeneratorRegistrationManager();
        readonly IAssemblyTraverser traverser;
        readonly TestGeneratorConfigurator configurator;

        public TestMethodGeneratorComposition(IAssemblyTraverser traverser, TestGeneratorConfigurator configurator, GeneratorRegistrationManager generatorRegistrationManager)
        {
            this.configurator = configurator;
            this.traverser = traverser;
            this.generatorRegistrationManager = generatorRegistrationManager;
        }

        public IEnumerable<ITestMethodGenerator> GetGenerators(Action<IContainer> typeRegistration,
            Action<ITestBuilderConfigurator> configure)
        {
            if (configure == null)
            {
                configurator.UseBuiltinGenerators();
            }
            else
            {
                configure(configurator);
            }

            Container container = SetupContainer(typeRegistration);

            var registrations = generatorRegistrationManager.GetGenerators().Select(producer => Lifestyle.Transient.CreateRegistration<ITestMethodGenerator>(() => producer(container), container)).ToArray();
            container.RegisterCollection(typeof(ITestMethodGenerator), registrations);
            container.Verify();

            var generators = container.GetAllInstances<ITestMethodGenerator>().ToArray();
            return generators;
        }

        private Container SetupContainer(Action<IContainer> typeRegistration)
        {
            var container = new Container();
            var wrapper = new ContainerWrapper(container);
            typeRegistration(wrapper);
            var registeredServices = wrapper.GetRegisteredServices();
            wrapper.RegisterWhenNotExists<IExpressionBuilder, ExpressionBuilder>();
            wrapper.RegisterWhenNotExists<IIdentifierValidator, CSharpIdentifierValidator>();
            wrapper.RegisterWhenNotExists<INullArgumentConstructorTestMethodSourceCodeGenerator, NullArgumentConstructorTestMethodSourceCodeGenerator>();
            wrapper.RegisterWhenNotExists<INullArgumentMethodTestMethodSourceCodeGenerator, NullArgumentMethodTestMethodSourceCodeGenerator>();
            wrapper.RegisterWhenNotExists<ITestMethodValueProvider>(configurator.GetTestMethodValueProvider());
            return container;
        }
    }
}

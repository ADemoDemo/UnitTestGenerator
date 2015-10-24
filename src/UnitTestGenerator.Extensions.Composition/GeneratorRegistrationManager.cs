using System;
using System.Collections.Generic;
using UnitTestGenerator.UnitTestGeneration;

namespace UnitTestGenerator.Extensions.Composition
{
    class GeneratorRegistrationManager
    {
        private IList<Func<IServiceProvider, ITestMethodGenerator>> generators = new List<Func<IServiceProvider, ITestMethodGenerator>>();

        public void AddGenerator<TGenerator>()
            where TGenerator : ITestMethodGenerator
        {
            generators.Add(x => (ITestMethodGenerator)x.GetService(typeof(TGenerator)));
        }

        public void AddGenerator(ITestMethodGenerator testMethodGenerator)
        {
            generators.Add(x => testMethodGenerator);
        }

        public void AddGenerator(Func<IServiceProvider, ITestMethodGenerator> instanceProducer)
        {
            generators.Add(instanceProducer);
        }

        public IEnumerable<Func<IServiceProvider, ITestMethodGenerator>> GetGenerators()
        {
            return generators;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGeneration.Extensions.Composition
{
    /// <summary>
    /// IoC container facade
    /// </summary>
    public interface IContainer
    {
        void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
        void Register<TService>(TService implementationInstance)
            where TService : class;
    }
}

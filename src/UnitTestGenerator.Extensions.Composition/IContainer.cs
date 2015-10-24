namespace UnitTestGenerator.Extensions.Composition
{
    /// <summary>
    /// IoC container facade
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers that an instance of TImplementation will be returned when an instance of type TService is requested.
        /// </summary>
        /// <typeparam name="TService">The interface or base type that can be used to retrieve the instances.</typeparam>
        /// <typeparam name="TImplementation">The concrete type that will be registered.</typeparam>
        void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Registers that the passed instance will be returned when an instance of type TService is requested.
        /// </summary>
        /// <typeparam name="TService">The interface or base type that can be used to retrieve the instances.</typeparam>
        /// <param name="implementationInstance">The passed instance which should be return.</param>
        void Register<TService>(TService implementationInstance)
            where TService : class;
    }
}

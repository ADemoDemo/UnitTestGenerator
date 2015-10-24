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

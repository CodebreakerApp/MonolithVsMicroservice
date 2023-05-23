using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace CodeBreaker.CLI.Infrastructure;

internal class TypeRegistrar(IServiceCollection services) : ITypeRegistrar
{
    public ITypeResolver Build()
    {
        return new TypeResolver(services.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        services.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        if (implementation == null)
            throw new ArgumentNullException(nameof(implementation));

        services.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        services.AddSingleton(service, (provider) => factory());
    }
}

file class TypeResolver(IServiceProvider serviceProvider) : ITypeResolver, IDisposable
{
    private readonly static EmptyCommandSettings _emptyCommandSettings = new();

    public object Resolve(Type? type)
    {
        if (type == null)
            throw new ArgumentOutOfRangeException(nameof(type));

        if (type == typeof(EmptyCommandSettings))
            return _emptyCommandSettings;

        return serviceProvider.GetRequiredService(type);
    }

    public void Dispose()
    {
        if (serviceProvider is IDisposable disposable)
            disposable.Dispose();
    }
}
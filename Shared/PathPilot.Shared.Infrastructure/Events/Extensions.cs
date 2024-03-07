using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Events;

namespace PathPilot.Shared.Infrastructure.Events;

internal static class Extensions
{
    internal static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
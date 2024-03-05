using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Shared.Infrastructure.Queries;

internal static class Extensions
{
    internal static IServiceCollection AddQueries(this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        services.Scan(x => x.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
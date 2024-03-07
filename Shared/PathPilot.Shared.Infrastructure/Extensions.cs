using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Infrastructure.Commands;
using PathPilot.Shared.Infrastructure.Events;
using PathPilot.Shared.Infrastructure.Queries;

namespace PathPilot.Shared.Infrastructure;

internal static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IList<Assembly> assemblies)
    {
        
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        //services.AddEvents(assemblies);
        // TODO: add in memory messaging

        return services;
    }
}
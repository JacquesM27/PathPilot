using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Modules;
using PathPilot.Shared.Infrastructure.Api;
using PathPilot.Shared.Infrastructure.Commands;
using PathPilot.Shared.Infrastructure.Modules;
using PathPilot.Shared.Infrastructure.Mongo;
using PathPilot.Shared.Infrastructure.Queries;

[assembly:InternalsVisibleTo("PathPilot.Bootstrapper")]
namespace PathPilot.Shared.Infrastructure;

internal static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IList<Assembly> assemblies, IList<IModule> modules)
    {

        services.AddModuleInfo(modules);
        services.AddModuleRequest(assemblies);
        
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        services.AddMongo();
        //services.AddEvents(assemblies);
        // TODO: add in memory messaging

        services
            .AddControllers()
            .DisableDisabledModules(services)
            .AddInternalControllersVisibility();

        return services;
    }
}
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Events;
using PathPilot.Shared.Abstractions.Modules;
using ICommand = System.Windows.Input.ICommand;

namespace PathPilot.Shared.Infrastructure.Modules;

public static class Extensions
{
    internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
    {
        var moduleInfoProvider = new ModuleInfoProvider();
        var moduleInfo = modules.Select(x =>
                             new ModuleInfo(x.Name, x.Path, x.Policies ?? Enumerable.Empty<string>())) ??
                         Enumerable.Empty<ModuleInfo>();
        
        moduleInfoProvider.Modules.AddRange(moduleInfo);
        services.AddSingleton(moduleInfoProvider);

        return services;
    }

    internal static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/modules", context =>
        {
            var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
            return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
        });
    }

    internal static IHostBuilder ConfigureModules(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            foreach (var settings in GetSettings("*"))
            {
                cfg.AddJsonFile(settings);
            }
            foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
            {
                cfg.AddJsonFile(settings);
            }

            IEnumerable<string> GetSettings(string pattern)
                => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);
        });
        return builder;
    }

    internal static IServiceCollection AddModuleRequest(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddModuleRegistry(assemblies);
        services.AddSingleton<IModuleClient, ModuleClient>();
        services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();
        services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();

        return services;
    }

    private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var registry = new ModuleRegistry();

        var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
        var eventTypes = types
            .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
            .ToArray();
        
        var commandTypes = types
            .Where(x => x.IsClass && typeof(ICommand).IsAssignableFrom(x))
            .ToArray();

        services.AddSingleton<IModuleRegistry>(sp =>
        {
            var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
            var eventDispatcherType = eventDispatcher.GetType();

            foreach (var eventType in eventTypes)
            {
                registry.AddBroadcastAction(eventType, @event =>
                    (Task)eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                        .MakeGenericMethod(eventType)
                        .Invoke(eventDispatcher, [@event]));
            }

            var commandDispatcher = sp.GetRequiredService<ICommandDispatcher>();
            var commandDispatcherType = commandDispatcher.GetType();

            foreach (var commandType in commandTypes)
            {
                registry.AddBroadcastAction(commandType, @event =>
                    (Task)commandDispatcherType.GetMethod(nameof(commandDispatcher.SendAsync))
                        .MakeGenericMethod(commandType)
                        .Invoke(commandDispatcher, [@event]));
            }

            return registry;
        });
    }
}
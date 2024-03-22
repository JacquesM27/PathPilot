using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PathPilot.Shared.Abstractions.Contexts;
using PathPilot.Shared.Abstractions.Modules;
using PathPilot.Shared.Abstractions.Storage;
using PathPilot.Shared.Infrastructure.Api;
using PathPilot.Shared.Infrastructure.Auth;
using PathPilot.Shared.Infrastructure.Commands;
using PathPilot.Shared.Infrastructure.Contexts;
using PathPilot.Shared.Infrastructure.Events;
using PathPilot.Shared.Infrastructure.Exceptions;
using PathPilot.Shared.Infrastructure.Messaging;
using PathPilot.Shared.Infrastructure.Modules;
using PathPilot.Shared.Infrastructure.Mongo;
using PathPilot.Shared.Infrastructure.Queries;
using PathPilot.Shared.Infrastructure.Storage;

[assembly:InternalsVisibleTo("PathPilot.Bootstrapper")]
namespace PathPilot.Shared.Infrastructure;

internal static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IList<Assembly> assemblies, IList<IModule> modules, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        
        services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.SectionName));
        // services.BindOptions<MongoOptions>(configuration, MongoOptions.SectionName);

        services.AddSwagger();
        
        services.AddMemoryCache();
        services.AddSingleton<IRequestStorage, RequestStorage>();

        services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());

        services.AddModuleInfo(modules);
        services.AddModuleRequest(assemblies);

        services.AddAuth(modules);
        services.AddErrorHandling();
        
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        
        services.AddEvents(assemblies);
        services.AddMessaging();

        services.AddMongoClient();
        
        services
            .AddControllers()
            .DisableDisabledModules(services)
            .AddInternalControllersVisibility();

        return services;
    }

    internal static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseErrorHandling();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseReDoc(options =>
        {
            options.RoutePrefix = "docs";
            options.SpecUrl("/swagger/v1/swagger.json");
            options.DocumentTitle = "PathPilot API";
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "PathPilot API",
                Version = "v1"
            });
        });
        return services;
    }
}
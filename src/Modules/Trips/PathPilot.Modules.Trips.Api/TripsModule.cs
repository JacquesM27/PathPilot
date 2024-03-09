using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Modules.Trips.Application;
using PathPilot.Modules.Trips.Domain;
using PathPilot.Modules.Trips.Infrastructure;
using PathPilot.Shared.Abstractions.Modules;

namespace PathPilot.Modules.Trips.Api;

internal sealed class TripsModule : IModule
{
    internal const string BasePath = "trips-module";
    public string Name => "Trips";
    public string Path => BasePath;

    public IEnumerable<string>? Policies { get; } = ["trips"];

    public void Register(IServiceCollection services)
    {
        services.AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        
    }
}
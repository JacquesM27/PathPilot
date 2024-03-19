using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Modules;

namespace PathPilot.Shared.Infrastructure.Auth;

internal static class Extensions
{
    internal static IServiceCollection AddAuth(this IServiceCollection services,
        IEnumerable<IModule>? modules = null, Action<JwtBearerOptions>? optionsFactory = null)
    {
        services.AddSingleton(TimeProvider.System);
        
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace PathPilot.Modules.Trips.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}
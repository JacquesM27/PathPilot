using Microsoft.Extensions.DependencyInjection;

namespace PathPilot.Modules.Trips.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
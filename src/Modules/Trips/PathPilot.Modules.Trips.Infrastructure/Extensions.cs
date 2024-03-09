using Microsoft.Extensions.DependencyInjection;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

namespace PathPilot.Modules.Trips.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRestaurantRepository();
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Modules.Trips.Application.Restaurants.Policies;

namespace PathPilot.Modules.Trips.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IRestaurantManagementPolicy, RestaurantManagementPolicy>();
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;
using PathPilot.Shared.Infrastructure.Mongo;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

internal static class RestaurantConfiguration
{

    internal static IServiceCollection AddRestaurantRepository(this IServiceCollection services)
    {
        services.AddMongoContext<RestaurantsMongoContext>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        return services;
    }
}
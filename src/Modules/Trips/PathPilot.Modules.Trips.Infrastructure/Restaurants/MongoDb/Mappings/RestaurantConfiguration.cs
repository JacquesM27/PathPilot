using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;
using PathPilot.Modules.Trips.Domain.Restaurants.Repositories;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Repositories;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

internal static class RestaurantConfiguration
{
    internal const string CollectionName = "Restaurants";

    internal static IServiceCollection AddRestaurantRepository(this IServiceCollection services)
    {
        ConfigureEntity();
        services.AddSingleton<IRestaurantRepository>(sp =>
        {
            var database = sp.GetService<IMongoDatabase>();
            return new RestaurantRepository(database!);
        });
        return services;
    }
    
    private static void ConfigureEntity()
    {
        BsonClassMap.RegisterClassMap<Restaurant>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        });
    }
}
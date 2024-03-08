using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using PathPilot.Modules.Trips.Domain.Restaurants.Entities;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;

internal static class RestaurantConfiguration
{
    internal const string CollectionName = "Restaurants";
    
    internal static void ConfigureEntity()
    {
        BsonClassMap.RegisterClassMap<Restaurant>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        });
    }
}
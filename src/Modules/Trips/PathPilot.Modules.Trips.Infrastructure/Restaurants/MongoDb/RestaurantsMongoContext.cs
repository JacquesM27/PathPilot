using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb.Mappings;
using PathPilot.Shared.Infrastructure.Mongo;

namespace PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb;

public sealed class RestaurantsMongoContext(
    IMongoClient mongoClient,
    IOptions<MongoOptions> options
    ) : MongoContext(mongoClient, options)
{
    private const string RestaurantsCollectionName = "Restaurants";
    
    internal IMongoCollection<RestaurantDocument> Restaurants
        => GetCollection<RestaurantDocument>(RestaurantsCollectionName);
}
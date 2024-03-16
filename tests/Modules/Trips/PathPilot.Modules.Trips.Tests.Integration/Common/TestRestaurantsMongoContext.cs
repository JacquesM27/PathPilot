using MongoDB.Driver;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb;
using PathPilot.Shared.Infrastructure.Mongo;
using PathPilot.Shared.Tests;

namespace PathPilot.Modules.Trips.Tests.Integration.Common;

public class TestRestaurantsMongoContext : IDisposable
{
    public RestaurantsMongoContext Context { get; }

    public TestRestaurantsMongoContext()
    {
        var options = OptionsHelper.GetOptions<MongoOptions>(MongoOptions.SectionName);
        var mongoClient = new MongoClient(options.ConnectionString);
        var restaurantContext = new RestaurantsMongoContext(mongoClient, new TestMongoOptions(options));
        Context =  restaurantContext;
    }

    public void Dispose()
    {
        Context.DropDatabase();
    }
}
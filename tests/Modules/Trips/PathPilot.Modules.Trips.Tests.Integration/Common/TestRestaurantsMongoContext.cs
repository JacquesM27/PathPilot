using MongoDB.Driver;
using PathPilot.Modules.Trips.Infrastructure.Restaurants.MongoDb;
using PathPilot.Shared.Infrastructure.Mongo;
using PathPilot.Shared.Tests;

namespace PathPilot.Modules.Trips.Tests.Integration.Common;

public class TestRestaurantsMongoContext : IDisposable
{
    public readonly RestaurantsMongoContext Context;
    private readonly MongoOptions _options;
    private readonly MongoClient _client;

    public TestRestaurantsMongoContext()
    {
        _options = OptionsHelper.GetOptions<MongoOptions>(MongoOptions.SectionName);
        _client = new MongoClient(_options.ConnectionString);
        var restaurantContext = new RestaurantsMongoContext(_client, new TestMongoOptions(_options));
        Context = restaurantContext;
    }

    public void Dispose()
    {
        DropDatabase();
        GC.SuppressFinalize(this);
    }

    private void DropDatabase()
    {
        _client.DropDatabase(_options.DatabaseName);
    }
}
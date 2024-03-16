using MongoDB.Driver;

namespace PathPilot.Shared.Tests;

public class MongoDbFixture : IDisposable
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbFixture()
    {
        //var options = services
    }
    
    public void Dispose()
    {
        // TODO release managed resources here
    }
}
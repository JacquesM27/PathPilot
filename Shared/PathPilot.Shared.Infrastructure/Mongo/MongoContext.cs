using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PathPilot.Shared.Infrastructure.Mongo;

public abstract class MongoContext
{
    private readonly IMongoDatabase _mongoDatabase;

    protected MongoContext(IMongoClient mongoClient, IOptions<MongoOptions> options)
    {
        var mongoOptions = options.Value;
        _mongoDatabase = mongoClient.GetDatabase(mongoOptions.DatabaseName);
    }

    protected IMongoCollection<T> GetCollection<T>(string collectionName)
        => _mongoDatabase.GetCollection<T>(collectionName);
}
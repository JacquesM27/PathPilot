using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PathPilot.Shared.Infrastructure.Mongo;

public abstract class MongoContext
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly MongoOptions _mongoOptions;

    protected MongoContext(IMongoClient mongoClient, IOptions<MongoOptions> options)
    {
        _mongoClient = mongoClient;
        _mongoOptions = options.Value;
        _mongoDatabase = mongoClient.GetDatabase(_mongoOptions.DatabaseName);
    }

    protected IMongoCollection<T> GetCollection<T>(string collectionName)
        => _mongoDatabase.GetCollection<T>(collectionName);

    public void DropDatabase()
        => _mongoClient.DropDatabase(_mongoOptions.DatabaseName);
}
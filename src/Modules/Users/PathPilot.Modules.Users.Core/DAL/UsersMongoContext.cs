using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PathPilot.Modules.Users.Core.DAL.Mappings;
using PathPilot.Shared.Infrastructure.Mongo;

namespace PathPilot.Modules.Users.Core.DAL;

public sealed class UsersMongoContext(
    IMongoClient mongoClient,
    IOptions<MongoOptions> options
    ) : MongoContext(mongoClient, options)
{
    private const string UsersCollectionName = "Users";

    internal IMongoCollection<UserDocument> Users
        => GetCollection<UserDocument>(UsersCollectionName);
}
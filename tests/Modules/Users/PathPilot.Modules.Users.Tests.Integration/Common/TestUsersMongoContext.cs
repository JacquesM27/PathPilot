using MongoDB.Driver;
using PathPilot.Modules.Users.Core.DAL;
using PathPilot.Shared.Infrastructure.Mongo;
using PathPilot.Shared.Tests;

namespace PathPilot.Modules.Users.Tests.Integration.Common;

public class TestUsersMongoContext : IDisposable
{
    public readonly UsersMongoContext Context;
    private readonly MongoOptions _options;
    private readonly MongoClient _client;

    public TestUsersMongoContext()
    {
        _options = OptionsHelper.GetOptions<MongoOptions>(MongoOptions.SectionName);
        _client = new MongoClient(_options.ConnectionString);
        var usersContext = new UsersMongoContext(_client, new TestMongoOptions(_options));
        Context = usersContext;
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
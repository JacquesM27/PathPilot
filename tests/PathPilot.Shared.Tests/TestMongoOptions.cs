using Microsoft.Extensions.Options;
using PathPilot.Shared.Infrastructure.Mongo;

namespace PathPilot.Shared.Tests;

public class TestMongoOptions(MongoOptions value) : IOptions<MongoOptions>
{
    public MongoOptions Value => value;
}
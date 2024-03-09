using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PathPilot.Shared.Infrastructure.Options;

namespace PathPilot.Shared.Infrastructure.Mongo;

internal static class Extensions
{
    internal static IServiceCollection AddMongo(this IServiceCollection services)
    {
        var options = services.GetOptions<MongoOptions>("mongo");

        services.AddSingleton(sp =>
        {
            var mongoClient = new MongoClient(options.ConnectionString);
            return mongoClient.GetDatabase(options.DatabaseName);
        });

        return services;
    }
}
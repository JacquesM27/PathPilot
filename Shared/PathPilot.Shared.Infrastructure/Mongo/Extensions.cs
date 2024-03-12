using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using PathPilot.Shared.Infrastructure.Options;

namespace PathPilot.Shared.Infrastructure.Mongo;

internal static class Extensions
{
    internal static IServiceCollection AddMongo(this IServiceCollection services)
    {
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);


        var options = services.GetOptions<MongoOptions>("MongoDB");

        services.AddSingleton(sp =>
        {
            var mongoClient = new MongoClient(options.ConnectionString);
            return mongoClient.GetDatabase(options.DatabaseName);
        });

        return services;
    }
}
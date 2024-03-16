using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using PathPilot.Shared.Infrastructure.Options;

namespace PathPilot.Shared.Infrastructure.Mongo;

public static class Extensions
{
    internal static IServiceCollection AddMongoClient(this IServiceCollection services)
    {
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);


        var options = services.GetOptions<MongoOptions>(MongoOptions.SectionName);

        var mongoClient = new MongoClient(options.ConnectionString);
        services.AddSingleton<IMongoClient, MongoClient>(_ => mongoClient);

        return services;
    }
    
    public static IServiceCollection AddMongoContext<T>(this IServiceCollection services)
        where T : MongoContext
    {
        services.AddScoped<T>();
        return services;
    }
}
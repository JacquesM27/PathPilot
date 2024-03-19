using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PathPilot.Shared.Infrastructure.Options;

public static class Extensions
{
    public static T GetOptions<T>(this IServiceCollection services, string sectionName)
        where T : class, new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    internal static T GetOptions<T>(this IConfiguration configuration, string sectionName)
        where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    internal static IServiceCollection BindOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
        where T : class
    {
        services.Configure<T>(configuration.GetSection(sectionName));
        return services;
    }
}
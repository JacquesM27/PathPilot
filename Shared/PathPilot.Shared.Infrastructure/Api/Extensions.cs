using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PathPilot.Shared.Infrastructure.Api;

internal static class Extensions
{
    internal static IMvcBuilder DisableDisabledModules(this IMvcBuilder mvcBuilder, IServiceCollection services)
    {
        mvcBuilder.ConfigureApplicationPartManager(manager =>
        {
            var removedParts = new List<ApplicationPart>();
            foreach (var disabledModule in GetDisabledModules(services))
            {
                var parts = manager.ApplicationParts
                    .Where(x => x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                removedParts.AddRange(parts);
            }

            foreach (var applicationPart in removedParts)
            {
                manager.ApplicationParts.Remove(applicationPart);
            }
        });
        return mvcBuilder;
    }

    internal static IMvcBuilder AddInternalControllersVisibility(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });
        return mvcBuilder;
    }

    private static IEnumerable<string> GetDisabledModules(IServiceCollection services)
    {
        var disabledModules = new List<string>();
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        foreach (var keyValuePair in configuration.AsEnumerable())
        {
            if (!keyValuePair.Key.Contains(":module:enabled"))
                continue;
                
            if (!bool.Parse(keyValuePair.Value))
                disabledModules.Add(keyValuePair.Key.Split(":")[0]);
        }

        return disabledModules;
    }
}
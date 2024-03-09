using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Modules;

namespace PathPilot.Shared.Infrastructure.Modules;

public class ModuleSubscriber(
    IModuleRegistry moduleRegistry,
    IServiceProvider serviceProvider
    ) : IModuleSubscriber
{
    public IModuleSubscriber Subscribe<TRequest, TResponse>(string path, Func<TRequest, 
        IServiceProvider, Task<TResponse>> action) where TRequest : class where TResponse : class
    {
        moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
            async request =>
            {
                using var scope = serviceProvider.CreateScope();
                return await action((TRequest)request, scope.ServiceProvider);
            });
        return this;
    }
}
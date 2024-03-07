using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Events;

namespace PathPilot.Shared.Infrastructure.Events;

internal sealed class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        using var scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

        var tasks = handlers.Select(task => task.HandleAsync(@event));
        await Task.WhenAll(tasks);
    }
}
using PathPilot.Shared.Abstractions.Messaging;
using PathPilot.Shared.Infrastructure.Messaging.Dispatchers;

namespace PathPilot.Shared.Infrastructure.Messaging.Brokers;

internal sealed class InMemoryMessageBroker(
    IAsyncMessageDispatcher asyncMessageDispatcher
    ) : IMessageBroker
{
    public async Task PublishAsync(params IMessage[] messages)
    {
        if (messages is null)
            return;

        messages = messages.Where(x => x is not null).ToArray();
        
        if (messages.Length == 0)
            return;

        foreach (var message in messages)
        {
            await asyncMessageDispatcher.PublishAsync(message);
        }
    }
}
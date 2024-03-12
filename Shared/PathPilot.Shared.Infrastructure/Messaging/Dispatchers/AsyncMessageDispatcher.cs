using PathPilot.Shared.Abstractions.Messaging;

namespace PathPilot.Shared.Infrastructure.Messaging.Dispatchers;

internal sealed class AsyncMessageDispatcher(IMessageChannel messageChannel) : IAsyncMessageDispatcher
{
    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    {
        await messageChannel.Writer.WriteAsync(message);
    }
}
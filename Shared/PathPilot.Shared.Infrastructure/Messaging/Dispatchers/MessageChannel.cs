using System.Threading.Channels;
using PathPilot.Shared.Abstractions.Messaging;

namespace PathPilot.Shared.Infrastructure.Messaging.Dispatchers;

internal sealed class MessageChannel : IMessageChannel
{
    private readonly Channel<IMessage> _messages = Channel.CreateUnbounded<IMessage>();

    public ChannelReader<IMessage> Reader => _messages.Reader;
    public ChannelWriter<IMessage> Writer => _messages.Writer;
}
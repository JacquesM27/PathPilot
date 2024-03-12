using System.Threading.Channels;
using PathPilot.Shared.Abstractions.Messaging;

namespace PathPilot.Shared.Infrastructure.Messaging.Dispatchers;

public interface IMessageChannel
{
    ChannelReader<IMessage> Reader { get; }
    ChannelWriter<IMessage> Writer { get; }
}
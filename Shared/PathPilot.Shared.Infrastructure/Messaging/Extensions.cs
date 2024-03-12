using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Messaging;
using PathPilot.Shared.Infrastructure.Messaging.Brokers;
using PathPilot.Shared.Infrastructure.Messaging.Dispatchers;

namespace PathPilot.Shared.Infrastructure.Messaging;

internal static class Extensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

        services.AddHostedService<BackgroundDispatcher>();

        return services;
    }
}
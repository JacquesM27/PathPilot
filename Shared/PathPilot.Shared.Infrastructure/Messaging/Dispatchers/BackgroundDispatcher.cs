using Microsoft.Extensions.Hosting;
using PathPilot.Shared.Abstractions.Modules;

namespace PathPilot.Shared.Infrastructure.Messaging.Dispatchers;

internal sealed class BackgroundDispatcher(
    IMessageChannel messageChannel,
    IModuleClient moduleClient
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in messageChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await moduleClient.PublishAsync(message);
            }
            catch (Exception exception)
            {
                // TODO: add logging
            }
        }
    }
}
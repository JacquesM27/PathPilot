using PathPilot.Shared.Abstractions.Modules;

namespace PathPilot.Shared.Infrastructure.Modules;

public class ModuleClient(
    IModuleRegistry moduleRegistry,
    IModuleSerializer moduleSerializer
    ) : IModuleClient
{
    public Task SendAsync(string path, object request)
        => SendAsync<object>(path, request);

    public async Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class
    {
        var registration = moduleRegistry.GetRequestRegistrations(path)
            ?? throw new InvalidOperationException($"No action has been defined for path: '{path}'.");

        var receiverRequest = TranslateType(request, registration.RequestType);
        var result = await registration.Action(receiverRequest);

        return result is null ? null : TranslateType<TResult>(result);
    }

    public async Task PublishAsync(object message)
    {
        var key = message.GetType().Name;
        var registrations = moduleRegistry.GetBroadcastRegistrations(key);

        var tasks = new List<Task>();
        
        foreach (var registration in registrations)
        {
            var action = registration.Action;
            var receiverMessage = TranslateType(message, registration.ReceiverType);
            
            tasks.Add(action(receiverMessage));
        }

        await Task.WhenAll(tasks);
    }

    private T TranslateType<T>(object value)
        => moduleSerializer.Deserialize<T>(moduleSerializer.Serialize(value));

    private object TranslateType(object value, Type type)
        => moduleSerializer.Deserialize(moduleSerializer.Serialize(value), type);
}
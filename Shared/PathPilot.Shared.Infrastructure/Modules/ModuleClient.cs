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

    public Task PublishAsync(object message)
    {
        throw new NotImplementedException();
    }

    private T TranslateType<T>(object value)
        => moduleSerializer.Deserialize<T>(moduleSerializer.Serialize(value));

    private object TranslateType(object value, Type type)
        => moduleSerializer.Deserialize(moduleSerializer.Serialize(value), type);
}
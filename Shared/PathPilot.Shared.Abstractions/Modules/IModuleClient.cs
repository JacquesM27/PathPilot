namespace PathPilot.Shared.Abstractions.Modules;

public interface IModuleClient
{
    Task SendAsync(string path, object request);
    Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class;
    Task PublishAsync(object message);
}
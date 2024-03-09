namespace PathPilot.Shared.Infrastructure.Modules;

public sealed class ModuleRequestRegistration(Type requestType, Type responseType, Func<object, Task<object>> action)
{
    public Type RequestType { get; } = requestType;
    public Type ResponseType { get; } = responseType;
    public Func<object, Task<object>> Action { get; } = action;
}
namespace PathPilot.Shared.Abstractions.Storage;

public interface IRequestStorage
{
    public void Set<T>(string key, T value, TimeSpan? duration = null);
    public T Get<T>(string key);
}
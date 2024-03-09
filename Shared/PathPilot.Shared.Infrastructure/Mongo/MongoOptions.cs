namespace PathPilot.Shared.Infrastructure.Mongo;

internal class MongoOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}
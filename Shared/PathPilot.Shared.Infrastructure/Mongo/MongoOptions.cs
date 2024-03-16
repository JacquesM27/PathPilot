namespace PathPilot.Shared.Infrastructure.Mongo;

public class MongoOptions
{
    public const string SectionName = "MongoDB";
    
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}
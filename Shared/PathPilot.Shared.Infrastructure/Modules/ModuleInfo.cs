namespace PathPilot.Shared.Infrastructure.Modules;

internal sealed record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
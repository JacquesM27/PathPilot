using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PathPilot.Shared.Abstractions.Modules;

public interface IModule
{
    public string Name { get; }
    public string Path { get; }
    IEnumerable<string>? Policies => null;

    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
}
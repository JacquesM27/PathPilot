using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Modules.Users.Core;
using PathPilot.Shared.Abstractions.Modules;

namespace PathPilot.Modules.Users.Api;

public sealed class UsersModule : IModule
{
    internal const string BasePath = "users-module";
    public string Name => "Users";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
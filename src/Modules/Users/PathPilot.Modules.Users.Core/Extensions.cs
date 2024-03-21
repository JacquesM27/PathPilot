using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Modules.Users.Core.DAL;
using PathPilot.Modules.Users.Core.DAL.Repositories;
using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.Repositories;
using PathPilot.Modules.Users.Core.Services;
using PathPilot.Shared.Infrastructure.Mongo;

namespace PathPilot.Modules.Users.Core;

internal static class Extensions
{
    internal static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMongoContext<UsersMongoContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}
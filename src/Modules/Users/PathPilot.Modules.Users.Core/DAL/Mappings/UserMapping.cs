using PathPilot.Modules.Users.Core.Entities;

namespace PathPilot.Modules.Users.Core.DAL.Mappings;

internal static class UserMapping
{
    internal static UserDocument ToDocument(this User user)
        => new()
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role,
            IsActive = user.IsActive,
            Claims = user.Claims
        };

    internal static User FromDocument(this UserDocument user)
        => new(user.Id,
            user.Email,
            user.Password,
            user.Role,
            user.IsActive,
            user.Claims);
}
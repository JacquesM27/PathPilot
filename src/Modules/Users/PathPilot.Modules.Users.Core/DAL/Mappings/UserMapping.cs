using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.ValueObjects;

namespace PathPilot.Modules.Users.Core.DAL.Mappings;

internal static class UserMapping
{
    internal static UserDocument ToDocument(this User user)
        => new()
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role,
            IsActive = user.IsActive,
            Claims = user.Claims
        };

    internal static User? FromDocument(this UserDocument? user)
        => user is null ? null : new User(user.Id,
            new Name(user.FirstName, user.LastName),
            user.Email,
            user.Password,
            user.Role,
            user.IsActive,
            user.Claims);
}
using PathPilot.Modules.Users.Core.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Users.Core.Entities;

public class User
{
    public EntityId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string Role { get; private set; }
    public bool IsActive { get; private set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; private set; }

    public User(EntityId id, Email email, Password password, string role, bool isActive,
        Dictionary<string, IEnumerable<string>> claims)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
        Claims = claims;
    }

    public static User CreateUser(Email email, Password password, string role, 
        Dictionary<string, IEnumerable<string>> claims)
        => new (Guid.NewGuid(), email, password, role, true, claims);
}
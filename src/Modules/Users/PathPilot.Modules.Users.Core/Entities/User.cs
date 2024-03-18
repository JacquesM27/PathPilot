using PathPilot.Modules.Users.Core.ValueObjects;
using PathPilot.Shared.Abstractions.Kernel.Types;

namespace PathPilot.Modules.Users.Core.Entities;

public class User
{
    public EntityId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
        = new Dictionary<string, IEnumerable<string>>();
}
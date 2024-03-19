namespace PathPilot.Modules.Users.Core.DTO;

public sealed class AccountDto(string email, string role, Dictionary<string, IEnumerable<string>> claims)
{
    public string Email { get; } = email;
    public string Role { get; } = role;
    public Dictionary<string, IEnumerable<string>> Claims { get; } = claims;
    public Guid Id { get; set; }
}
namespace PathPilot.Modules.Users.Core.DTO;

public sealed class SignUpDto(string email, string password, string role, Dictionary<string, IEnumerable<string>> claims)
{
    public Guid Id { get; set; }
    public string Email { get; } = email;
    public string Password { get; } = password;
    public string Role { get; } = role;
    public Dictionary<string, IEnumerable<string>> Claims { get; } = claims;
}
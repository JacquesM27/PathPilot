namespace PathPilot.Modules.Users.Core.DTO;

public sealed class SignUpDto(string email, string firstName, string lastName, string password, Dictionary<string, IEnumerable<string>> claims)
{
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Password { get; } = password;
    public Dictionary<string, IEnumerable<string>> Claims { get; } = claims;
}
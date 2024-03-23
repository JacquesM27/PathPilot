namespace PathPilot.Modules.Users.Core.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    private Password(string value)
    {
        Value = value;
    }
    
    public static implicit operator Password(string password) => new(password);
    public static implicit operator string(Password password) => password.Value;
}
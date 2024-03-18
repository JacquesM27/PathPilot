using PathPilot.Modules.Users.Core.Exceptions;

namespace PathPilot.Modules.Users.Core.ValueObjects;

public sealed record Password
{
    public string Value { get; private set; }

    public Password(string value)
    {
        //TODO: move this to "service"
        if (!HasPasswordValidPolicy(value))
            throw new InvalidPasswordException();
        
        Value = value;
    }
    
    private static bool HasPasswordValidPolicy(string password)
    {
        if (password.Length < 6)
            return false;

        return password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit) &&
               password.Any(IsSpecialCharacter);
    }

    private static bool IsSpecialCharacter(char c)
    {
        return "!@#$%^&*()_+-=[]{};':\"\\|,.<>?".Contains(c);
    }
    
    public static implicit operator Password(string password) => new(password);
    public static implicit operator string(Password password) => password.Value;
}
using System.Text.RegularExpressions;
using PathPilot.Modules.Users.Core.Exceptions;

namespace PathPilot.Modules.Users.Core.ValueObjects;

public sealed record Email
{
    private static readonly Regex Regex = new(
        @"^(?=.*[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,})[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$",
        RegexOptions.Compiled);
    
    public string Value { get; }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value))
            throw new InvalidEmailAddressException(value);
        
        Value = value;
    }

    public static implicit operator Email(string value) => new(value);
    public static implicit operator string(Email email) => email.Value;
}
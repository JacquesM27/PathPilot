using System.Text.RegularExpressions;
using PathPilot.Modules.Users.Core.Exceptions;

namespace PathPilot.Modules.Users.Core.ValueObjects;

public sealed record Email
{
    private static readonly Regex Regex = new(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\\+/=\?\^`\{\}\|~\w]))(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z][0-9a-z]\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);
    
    public string Value { get; init; }

    public Email(string value)
    {
        if (!Regex.IsMatch(value))
            throw new InvalidEmailAddressException(value);
        
        Value = value;
    }

    public static implicit operator Email(string value) => new(value);
    public static implicit operator string(Email email) => email.Value;
}
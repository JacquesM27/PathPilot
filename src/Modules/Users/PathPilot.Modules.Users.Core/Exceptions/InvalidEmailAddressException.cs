using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class InvalidEmailAddressException(string email)
    : PathPilotException($"User defines invalid email:  '{email}'.")
{
    public string Email => email;
}
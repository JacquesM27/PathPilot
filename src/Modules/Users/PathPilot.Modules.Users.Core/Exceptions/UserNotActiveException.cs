using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class UserNotActiveException(Guid id)
    : PathPilotException($"User with ID : '{id}' is not active.")
{
    public Guid Id => id;
}
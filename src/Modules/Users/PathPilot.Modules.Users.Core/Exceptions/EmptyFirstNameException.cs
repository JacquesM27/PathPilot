using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class EmptyFirstNameException()
    : PathPilotException($"User defines empty first name.")
{
}
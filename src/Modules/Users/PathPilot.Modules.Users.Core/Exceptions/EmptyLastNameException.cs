using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class EmptyLastNameException()
    : PathPilotException($"User defines empty last name exception")
{
    
}
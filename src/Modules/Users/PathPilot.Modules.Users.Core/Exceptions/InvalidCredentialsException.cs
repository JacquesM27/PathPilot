using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class InvalidCredentialsException()
    : PathPilotException("Invalid email or password.");
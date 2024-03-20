using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class EmailInUseException()
    : PathPilotException("Email is already in use.");
using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Users.Core.Exceptions;

public sealed class InvalidPasswordException()
    : PathPilotException($"The password does not meet security requirements. It should have a minimum of 6 " +
                         $"characters, contain lowercase and uppercase letters, numbers and special characters");
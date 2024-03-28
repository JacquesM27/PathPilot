using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

public sealed class InvalidStarsValueException(int value, int minimum, int maximum)
    : PathPilotException($"Accommodation defines incorrect value of stars: '{value}'. " +
                         $"The value should be in the range from {minimum} to {maximum}");
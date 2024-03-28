using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Modules.Trips.Domain.Accommodation.Exceptions;

public sealed class InvalidAverageRatingAmountException(double value, double minimum, double maximum)
    : PathPilotException($"Accommodation defines incorrect value of average rating: '{value}'. " +
                         $"The value should be in the range from {minimum} to {maximum}");
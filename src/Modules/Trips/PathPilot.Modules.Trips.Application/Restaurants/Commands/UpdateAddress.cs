﻿using PathPilot.Shared.Abstractions.Commands;

namespace PathPilot.Modules.Trips.Application.Restaurants.Commands;

public sealed record UpdateAddress(string RestaurantId, string City, string Street, string BuildingNumber, 
    string PostCode, string Country) : ICommand;
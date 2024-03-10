using Microsoft.AspNetCore.Mvc;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Queries;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Modules.Trips.Api.Controllers;

internal class RestaurantsController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher
    ) : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<RestaurantDetailsDto>> GetAsync(string id)
        => OkOrNotFound(await queryDispatcher.QueryAsync(new GetRestaurant(id)));

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IReadOnlyList<RestaurantDto>>> BrowseAsync()
        => Ok(await queryDispatcher.QueryAsync(new BrowseRestaurants()));

    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<ActionResult> AddAsync(CreateRestaurant command)
    {
        await commandDispatcher.SendAsync(command);
        return NoContent();
        //TODO: add in memory cache with id and return CreatedAtAction
    }
}
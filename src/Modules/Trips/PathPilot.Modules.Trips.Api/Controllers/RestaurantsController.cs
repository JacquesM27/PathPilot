using Microsoft.AspNetCore.Mvc;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Queries;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Queries;
using PathPilot.Shared.Abstractions.Storage;

namespace PathPilot.Modules.Trips.Api.Controllers;

internal class RestaurantsController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher,
    IRequestStorage requestStorage
    ) : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<RestaurantDetailsDto>> Get(string id)
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
        var id = requestStorage.Get<string>(command.CommandId);
        return CreatedAtAction(nameof(Get), id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> CloseAsync(string id)
    {
        //TODO: add closing policy only for owner - when user module will be completed
        await commandDispatcher.SendAsync(new CloseRestaurant(id));
        return NoContent();
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> OpenAsync(string id)
    {
        await commandDispatcher.SendAsync(new OpenRestaurant(id));
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateAddressAsync(UpdateAddress command)
    {
        await commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateMenuAsync(UpdateMenu command)
    {
        await commandDispatcher.SendAsync(command);
        return NoContent();
    }
}
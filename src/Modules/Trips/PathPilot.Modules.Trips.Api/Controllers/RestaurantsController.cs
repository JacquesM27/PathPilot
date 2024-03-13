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
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<RestaurantDetailsDto>> Get(Guid id)
        => OkOrNotFound(await queryDispatcher.QueryAsync(new GetRestaurant(id)));

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IReadOnlyList<RestaurantDto>>> BrowseAsync()
        => Ok(await queryDispatcher.QueryAsync(new BrowseRestaurants()));

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult> AddAsync(CreateRestaurant command)
    {
        await commandDispatcher.SendAsync(command);
        return CreatedAtAction(nameof(Get), command.Id);
    }
    
    [HttpPost("detailed")]
    [ProducesResponseType(201)]
    public async Task<ActionResult> AddAsync(CreateDetailedRestaurant command)
    {
        await commandDispatcher.SendAsync(command);
        return CreatedAtAction(nameof(Get), command.Id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> CloseAsync(Guid id)
    {
        //TODO: add closing policy only for owner - when user module will be completed
        await commandDispatcher.SendAsync(new CloseRestaurant(id));
        return NoContent();
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> OpenAsync(Guid id)
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
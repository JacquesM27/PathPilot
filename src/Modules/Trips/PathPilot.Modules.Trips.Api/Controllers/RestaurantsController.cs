using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathPilot.Modules.Trips.Application.Restaurants.Commands;
using PathPilot.Modules.Trips.Application.Restaurants.DTO;
using PathPilot.Modules.Trips.Application.Restaurants.Queries;
using PathPilot.Shared.Abstractions.Commands;
using PathPilot.Shared.Abstractions.Contexts;
using PathPilot.Shared.Abstractions.Queries;

namespace PathPilot.Modules.Trips.Api.Controllers;

internal class RestaurantsController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher,
    IContext context
    ) : BaseController
{
    //TODO: add policy only for owner - when user module will be completed
    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<RestaurantDetailsDto?>> Get(Guid id)
        => OkOrNotFound(await queryDispatcher.QueryAsync(new GetRestaurant(id)));

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IReadOnlyList<RestaurantDto>>> BrowseAsync()
        => Ok(await queryDispatcher.QueryAsync(new BrowseRestaurants()));

    [Authorize]
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> AddAsync(CreateRestaurant command)
    {
        command.OwnerId = context.Identity.Id;
        await commandDispatcher.SendAsync(command);
        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }
    
    [Authorize]
    [HttpPost("detailed")]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> AddAsync(CreateDetailedRestaurant command)
    {
        command.OwnerId = context.Identity.Id;
        await commandDispatcher.SendAsync(command);
        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }

    [Authorize]
    [HttpPut("close/{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> CloseAsync(Guid id)
    {
        await commandDispatcher.SendAsync(new CloseRestaurant(id));
        return NoContent();
    }
    
    [Authorize]
    [HttpPut("open/{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> OpenAsync(Guid id)
    {
        await commandDispatcher.SendAsync(new OpenRestaurant(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("new-address")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateAddressAsync(UpdateAddress command)
    {
        await commandDispatcher.SendAsync(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut("new-menu")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateMenuAsync(UpdateMenu command)
    {
        await commandDispatcher.SendAsync(command);
        return NoContent();
    }
}
using DiningWheels.Application.Restaurants.Commands;
using DiningWheels.Application.Restaurants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiningWheels.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantDto>>> GetRestaurants()
    {
        var restaurants = await _mediator.Send(new GetRestaurantsQuery());
        return Ok(restaurants);       
    }
}
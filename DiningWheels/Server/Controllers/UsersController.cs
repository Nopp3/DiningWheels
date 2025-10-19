using DiningWheels.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiningWheels.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateUserCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);       
    }
}
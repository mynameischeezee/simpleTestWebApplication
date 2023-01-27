using MediatR;
using Microsoft.AspNetCore.Mvc;
using moviesStorage.IdentityService.Commands;
using moviesStorage.IdentityService.Queries;

namespace moviesStorage.IdentityService.Controllers;

[ApiController]
[Route("/auth")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("~/login")]
    public async Task<IActionResult> Login()
    {
        var command = new LoginUserCommand();
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("~/logout")]
    public async Task<IActionResult> Logout()
    {
        var command = new LogoutUserQuery();
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
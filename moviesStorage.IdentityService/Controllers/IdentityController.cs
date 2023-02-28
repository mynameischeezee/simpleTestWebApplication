using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using moviesStorage.IdentityService.Commands;
using moviesStorage.IdentityService.Filters.LoginFilters;
using moviesStorage.IdentityService.Filters.LogoutFilters;
using moviesStorage.IdentityService.Models;
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
    [LoginActionFilter]
    [LoginErrorFilter]
    [Route("~/login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
    {
        var command = new LoginUserCommand(user.Username, user.Password);
        var result = await _mediator.Send(command);
        var token = await HttpContext.GetTokenAsync("access_token");
        return Ok(result);
    }
    
    [HttpGet]
    [LogoutActionFilter]
    [LogoutErrorFilter]
    [Route("~/logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var command = new LogoutUserQuery();
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
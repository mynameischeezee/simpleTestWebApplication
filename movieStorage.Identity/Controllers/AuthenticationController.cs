
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using movieStorage.Identity.Commands;
using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.Exceptions;
using movieStorage.Identity.Filters;
using movieStorage.Identity.Models;

namespace movieStorage.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [RegistrationActionFilter]
    [RegistrationErrorFilter]
    [Route("~/register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
    {
        var command = new RegisterUserCommand(userDTO);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("~/login")]
    public async Task<IActionResult> Login()
    {
        return null;
    }
    
    [HttpGet]
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        return null;
    }
}
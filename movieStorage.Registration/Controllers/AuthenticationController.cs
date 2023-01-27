
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using movieStorage.Registration.Data.Identity;
using movieStorage.Registration.Exceptions;
using movieStorage.Registration.Commands;
using movieStorage.Registration.Filters;
using movieStorage.Registration.Models;

namespace movieStorage.Registration.Controllers;

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
}
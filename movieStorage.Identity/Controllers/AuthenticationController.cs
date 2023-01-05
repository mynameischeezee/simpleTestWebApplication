
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.Exceptions;
using movieStorage.Identity.Filters;
using movieStorage.Identity.Models;

namespace movieStorage.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ServiceUser> _userManager;
    private readonly SignInManager<ServiceUser> _signInManager;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IMapper _mapper;
    
    public AuthenticationController(UserManager<ServiceUser> userManager,
        SignInManager<ServiceUser> signInManager,
        ILogger<AuthenticationController> logger,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _mapper = mapper;
    }
    
    [HttpPost]
    [RegistrationActionFilter]
    [RegistrationErrorFilter]
    [Route("~/register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
    { 
        if (!ModelState.IsValid)
        {
            throw new ModelInvalidException(ModelState.ToString());
        }
        var user = _mapper.Map<ServiceUser>(userDTO);
        var result = await _userManager.CreateAsync(user, userDTO.Password);
        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors);
        }
        return Ok($"User {userDTO.Username} registered successfully");
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
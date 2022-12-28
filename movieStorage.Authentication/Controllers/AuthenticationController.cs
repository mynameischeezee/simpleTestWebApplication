using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using movieStorage.Authentication.Models;
using movieStorage.Authentication.Data.Identity;

namespace movieStorage.Authentication.Controllers;

[ApiController]
[Route("auth/[controller]")]
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
    [Route("~/register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
    {
        _logger.LogInformation($"Attempting to register user {userDTO.Username} with email address {userDTO.Email}");
        if (!ModelState.IsValid)
        {
            _logger.LogError($"Model state for {nameof(Register)} is not valid");
            return BadRequest(ModelState);
        }

        try
        {
            var user = _mapper.Map<ServiceUser>(userDTO);
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError($"Error while attempting to create a new user. {result.Errors}");
                return BadRequest("User registration fail.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong in {nameof(Register)}");
            _logger.LogError($"Error message: {e.Message}");
            return Problem($"Something went wrong in {nameof(Register)}", statusCode: 500);
        }
            
        return null;
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
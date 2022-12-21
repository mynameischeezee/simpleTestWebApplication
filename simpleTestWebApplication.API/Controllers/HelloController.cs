using Microsoft.AspNetCore.Mvc;

namespace simpleTestWebApplication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    [Route("/Hello/Test")]
    public IActionResult Get()
    {
        return Ok("Hello world");
    }
}
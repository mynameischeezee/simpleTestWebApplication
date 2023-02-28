using MediatR;
using Microsoft.AspNetCore.Mvc;
using moviesStorage.IdentityService.Queries;

namespace moviesStorage.IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class LocalController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("~/user/session")]
    public async Task<IActionResult> Session()
    {
        var command = new GetSessionQuery();
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
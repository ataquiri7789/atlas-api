using AtlasCommerce.Application.Features.Authentication.Commands.Login;
using AtlasCommerce.Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AtlasCommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    public AuthController(IMediator mediator)
        : base(mediator)
    {
    }


    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResponse(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResponse(result);
    }
}
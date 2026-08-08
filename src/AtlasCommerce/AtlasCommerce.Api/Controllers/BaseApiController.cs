using AtlasCommerce.Application.Common.Errors;
using AtlasCommerce.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AtlasCommerce.Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IActionResult HandleResponse<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Type switch
        {
            ErrorType.Validation => BadRequest(result.Error),

            ErrorType.NotFound => NotFound(result.Error),

            ErrorType.Conflict => Conflict(result.Error),

            ErrorType.Unauthorized => Unauthorized(result.Error),

            ErrorType.Forbidden => Forbid(),

            _ => BadRequest(result.Error)
        };
    }
}
using AtlasCommerce.Application.Features.Customers.Commands.CreateCustomer;
using AtlasCommerce.Application.Features.Customers.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtlasCommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(Create), new { id }, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _mediator.Send(new GetCustomersQuery());

        return Ok(customers);
    }
}
using AtlasCommerce.Application.Features.Customers.DTOs;
using MediatR;

namespace AtlasCommerce.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
}
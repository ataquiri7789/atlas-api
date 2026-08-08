using AtlasCommerce.Application.Constants;
using AtlasCommerce.Application.Features.Customers.DTOs;
using AtlasCommerce.Application.Interfaces;
using AtlasCommerce.Domain.Repositories;
using MediatR;

namespace AtlasCommerce.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICacheService _cacheService;

    public GetCustomersHandler(
        ICustomerRepository customerRepository,
        ICacheService cacheService)
    {
        _customerRepository = customerRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        // 1. Buscar en Redis
        var cachedCustomers =
            await _cacheService.GetAsync<List<CustomerDto>>(CacheKeys.Customers);

        if (cachedCustomers is not null)
        {
            Console.WriteLine("📦 Obteniendo clientes desde Redis...");
            return cachedCustomers;
        }

        // 2. Consultar PostgreSQL
        var customers = await _customerRepository.GetAllAsync();

        Console.WriteLine("🗄️ Obteniendo clientes desde PostgreSQL...");
        // 3. Mapear a DTO
        var customerDtos = customers.Select(customer => new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone
        }).ToList();

        // 4. Guardar en Redis por 30 minutos
        await _cacheService.SetAsync(
            CacheKeys.Customers,
            customerDtos,
            TimeSpan.FromMinutes(30));

        // 5. Retornar respuesta
        return customerDtos;
    }
}
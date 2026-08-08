using AtlasCommerce.Domain.Entities;
using AtlasCommerce.Domain.Repositories;
using AtlasCommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AtlasCommerce.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AtlasCommerceDbContext _context;

    public CustomerRepository(AtlasCommerceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);

    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }
}
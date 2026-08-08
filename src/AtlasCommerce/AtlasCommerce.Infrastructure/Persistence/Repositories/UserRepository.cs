using AtlasCommerce.Domain.Entities;
using AtlasCommerce.Domain.Repositories;
using AtlasCommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AtlasCommerce.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AtlasCommerceDbContext _context;

    public UserRepository(AtlasCommerceDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
}
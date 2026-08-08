using AtlasCommerce.Domain.Entities;

namespace AtlasCommerce.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task AddAsync(User user);
}
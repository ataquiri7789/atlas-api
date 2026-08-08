using AtlasCommerce.Domain.Repositories;
using AtlasCommerce.Infrastructure.Persistence.Context;

namespace AtlasCommerce.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AtlasCommerceDbContext _context;

    public UnitOfWork(AtlasCommerceDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
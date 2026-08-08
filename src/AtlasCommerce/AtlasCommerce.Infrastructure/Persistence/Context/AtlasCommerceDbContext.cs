using AtlasCommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AtlasCommerce.Infrastructure.Persistence.Context;

public class AtlasCommerceDbContext : DbContext
{
    public AtlasCommerceDbContext(DbContextOptions<AtlasCommerceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<User> Users { get; set; }

    //"Busca todas las clases que implementen IEntityTypeConfiguration<>
    //y aplícalas automáticamente."
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtlasCommerceDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }


}
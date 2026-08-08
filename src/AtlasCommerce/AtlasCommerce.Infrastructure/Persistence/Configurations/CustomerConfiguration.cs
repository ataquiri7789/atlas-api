using AtlasCommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtlasCommerce.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(x => x.Phone)
               .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .IsRequired();
    }
}
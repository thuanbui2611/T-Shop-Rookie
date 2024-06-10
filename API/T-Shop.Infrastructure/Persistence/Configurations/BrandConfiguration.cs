using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("table_brand");

        //builder.Navigation(p => p.Models).AutoInclude();

        // Brand - Model
        builder.HasMany(b => b.Models)
            .WithOne(m => m.Brand)
            .HasForeignKey(m => m.BrandID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

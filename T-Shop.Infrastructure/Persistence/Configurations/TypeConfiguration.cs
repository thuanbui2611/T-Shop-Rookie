using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class TypeConfiguration : IEntityTypeConfiguration<Domain.Entity.TypeProduct>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.TypeProduct> builder)
    {
        builder.ToTable("table_type");

        //Type - Product
        builder.HasMany(t => t.Products)
            .WithOne(b => b.Type)
            .HasForeignKey(b => b.TypeID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

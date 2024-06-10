using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("table_model");

        //Model - Product
        builder.HasMany(m => m.Products)
            .WithOne(p => p.Model)
            .HasForeignKey(p => p.ModelID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole
            {
                Id = Guid.Parse("55ea57a5-5eb0-446a-bd16-54de57738815"),
                Name = "User",
                NormalizedName = "USER"
            },
            new ApplicationRole
            {
                Id = Guid.Parse("d2145b7c-6d76-4eb9-9e6c-ab101b754c72"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
    }
}

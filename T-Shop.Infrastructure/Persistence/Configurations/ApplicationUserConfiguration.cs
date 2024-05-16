using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // User - Image
        builder.HasOne(u => u.Image)
            .WithOne()
            .HasForeignKey<ApplicationUser>(u => u.AvatarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
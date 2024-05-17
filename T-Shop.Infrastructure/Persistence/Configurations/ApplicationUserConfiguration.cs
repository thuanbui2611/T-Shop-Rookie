using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

    }
}
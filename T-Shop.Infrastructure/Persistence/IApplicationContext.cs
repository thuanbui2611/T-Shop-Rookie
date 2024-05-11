using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
    }
}

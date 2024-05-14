using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<TypeProduct> Types { get; set; }
    }
}

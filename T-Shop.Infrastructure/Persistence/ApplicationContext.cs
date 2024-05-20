using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Infrastructure.Persistence.Constant;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Infrastructure.Persistence
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<TypeProduct> Types { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(builder);
            //SeedDatabase(builder);

            //Delete "AspNet" name of identity table
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tablename = entityType.GetTableName();
                if (tablename.StartsWith("AspNet"))
                {
                    entityType.SetTableName($"table_{tablename[6..].ToLower()}");
                }
            }
        }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(SeedDatabaseConstant.Default_Product);
        }

        public DbSet<T> GetSet<T>()
            where T : class
        {
            return Set<T>();
        }
    }
}

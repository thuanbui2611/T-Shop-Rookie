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
        public DbSet<Category> Categories { get; set; }

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
            modelBuilder.Entity<Category>().HasData(SeedDatabaseConstant.Default_Category);
            modelBuilder.Entity<Product>().HasData(SeedDatabaseConstant.Default_Product);
        }

        public DbSet<T> GetSet<T>()
            where T : class
        {
            return Set<T>();
        }
    }
}

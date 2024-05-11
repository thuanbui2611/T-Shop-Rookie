using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Data.Queries;
using T_Shop.Infrastructure.Data.Repository;
using T_Shop.Infrastructure.Persistence;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);
        services.ConfigureIdentity();
        services.RegisterQueriesDependencies();
        services.RegistryDatabaseDependencies();

        return services;
    }
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        //Database connection Postgres db
        services.AddDbContextPool<ApplicationContext>(
            option => option.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")!,
            b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
           //.UseModel(ApplicationContextModel.Instance)
           );
        ;
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 10;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();
    }


    public static void RegisterQueriesDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProductQueries, ProductQueries>();
        services.AddScoped<ICategoryQueries, CategoryQueries>();
    }

    public static void RegistryDatabaseDependencies(this IServiceCollection services)
    {
        services.AddScoped<IApplicationContext, ApplicationContext>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}


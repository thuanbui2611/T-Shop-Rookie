using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using T_Shop.Application.Common.Interface;
using T_Shop.Application.Common.ServiceInterface;
using T_Shop.Domain.Entity.ServiceEntity.Cloudinary;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Data.Queries;
using T_Shop.Infrastructure.Data.Repository;
using T_Shop.Infrastructure.Persistence;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Infrastructure.SharedServices.Authentication;
using T_Shop.Infrastructure.SharedServices.Cloudinary;

namespace T_Shop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);
        //Identity
        //services.ConfigureJWT(configuration);
        services.ConfigureIdentity();
        //DI
        services.RegisterQueriesDependencies();
        services.RegistryDatabaseDependencies();
        services.RegisterServices(configuration);

        return services;
    }
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        //Database connection Postgres db
        services.AddDbContextPool<ApplicationContext>(
            option => option.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")!,
            b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
           //.UseModel(ApplicationContextModel.Instance)
           );
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 6;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();
    }
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["secret"];
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }

    public static void RegisterQueriesDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProductQueries, ProductQueries>();
        services.AddScoped<IBrandQueries, BrandQueries>();
        services.AddScoped<IColorQueries, ColorQueries>();
        services.AddScoped<IModelQueries, ModelQueries>();
        services.AddScoped<ITypeQueries, TypeQueries>();
    }

    public static void RegistryDatabaseDependencies(this IServiceCollection services)
    {
        services.AddScoped<IApplicationContext, ApplicationContext>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountManager, AccountManager>();
        //Cloudinary
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddScoped<ICloudinaryService, CloudinaryService>();
    }
}


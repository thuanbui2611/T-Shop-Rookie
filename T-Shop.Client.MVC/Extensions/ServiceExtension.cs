using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Repository.Repository;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Client.MVC.Services.Services;

namespace T_Shop.Client.MVC.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddHttpClient<IProductRepository, ProductRepository>();
            services.AddHttpClient<IBrandRepository, BrandRepository>();
            services.AddHttpClient<ITypeRepository, TypeRepository>();
            services.AddHttpClient<IColorRepository, ColorRepository>();
            services.AddHttpClient<IModelRepository, ModelRepository>();
            services.AddHttpClient<ICartRepository, CartRepository>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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
                    ValidIssuer = configuration["JwtSettings:validIssuer"],
                    ValidAudience = configuration["JwtSettings:validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:secret"]))
                };
            });

            //services.AddAuthorization();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/authentication/login";
            //    options.AccessDeniedPath = "/authentication/AccessDenied";
            //});
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .WithExposedHeaders("Authorization");
                    });
            });
        }
    }
}

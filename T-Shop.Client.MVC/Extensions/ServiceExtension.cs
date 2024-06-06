using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Repository.Repository;
using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Client.MVC.Services.Services;

namespace T_Shop.Client.MVC.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();


            var configureClient = new Action<IServiceProvider, HttpClient>((provider, client) =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

                string? accessToken = null;
                httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue("AuthToken", out accessToken);
                client.BaseAddress = new Uri(configuration["ApiConnectionString"] ?? "");

                if (accessToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

            });

            services.AddHttpClient<IAuthenticationRepository, AuthenticationRepository>(configureClient);
            services.AddHttpClient<IProductRepository, ProductRepository>(configureClient);
            services.AddHttpClient<IBrandRepository, BrandRepository>(configureClient);
            services.AddHttpClient<ITypeRepository, TypeRepository>(configureClient);
            services.AddHttpClient<IColorRepository, ColorRepository>(configureClient);
            services.AddHttpClient<IModelRepository, ModelRepository>(configureClient);
            services.AddHttpClient<ICartRepository, CartRepository>(configureClient);
            services.AddHttpClient<IOrderRepository, OrderRepository>(configureClient);
            services.AddHttpClient<ITransactionRepository, TransactionRepository>(configureClient);
            services.AddHttpClient<IUserRepository, UserRepository>(configureClient);
        }


        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = configuration["JwtSettings:validIssuer"],
            //        ValidAudience = configuration["JwtSettings:validAudience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:secret"]))
            //    };
            //});

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });


            //    .AddCookie(options =>
            //{
            //    options.LoginPath = "/authentication/Login";
            //    options.AccessDeniedPath = "/authentication/AccessDenied";
            //});

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

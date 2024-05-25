using T_Shop.Client.MVC.Services.Interfaces;
using T_Shop.Client.MVC.Services.Services;

namespace T_Shop.Client.MVC.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient("BaseHttpClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001");
            });

            services.AddHttpClient<IProductRepository, ProductRepository>();
            services.AddHttpClient<IBrandRepository, BrandRepository>();
            services.AddHttpClient<ITypeRepository, TypeRepository>();
            services.AddHttpClient<IColorRepository, ColorRepository>();
            services.AddHttpClient<IModelRepository, ModelRepository>();
        }
    }
}

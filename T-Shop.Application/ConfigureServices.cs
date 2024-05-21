using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using T_Shop.Application.Common.Behaviours;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Common.Mappings;

namespace T_Shop.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(ctg =>
            {
                ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //Validation
                //ctg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            //fluent validation
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //logger
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

            services.ConfigureCache();

            return services;
        }

        public static void ConfigureCache(this IServiceCollection services)
        {
            services.AddLazyCache();
            services.AddSingleton<CacheKeyConstants>();
        }

    }
}

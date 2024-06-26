﻿using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
             services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy", builder =>
                 builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
             });
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var startTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).ToString();

                var swaggerDescription = "## Description. \n\n" +
                "- This is a list of sample APIs\n\n" +
                "\n\n" +
                $"* Last updated at: __{startTime}__ \n\n";

                OpenApiInfo apiInfo = new OpenApiInfo
                {
                    Title = "T-Shop Swagger",
                    Description = swaggerDescription,
                    Version = "develop"
                };
                options.SwaggerDoc("v1", apiInfo);

                // Generating api description via xml;
                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                // Add authentication button
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter your token here. The token is in JWT format",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddPaginationHeader(this HttpResponse response, PaginationMetaData pagination)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            response.Headers.Add("Access-Control-Allow-Origin", "*");

            response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination, options));

            response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        }
    }
}

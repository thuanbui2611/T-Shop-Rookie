using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity.Exceptions;

namespace T_Shop.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");
                        if (contextFeature.Error is ValidationAppException exception)
                        {

                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                exception.Errors
                            }));
                        }
                        else
                        {
                            await context.Response.WriteAsync(new ErrorResponseModel()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                            }.ToString());
                        }
                    }
                });
            });
        }

    }

}

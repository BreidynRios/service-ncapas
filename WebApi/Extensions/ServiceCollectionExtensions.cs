using BusinessLogic.Commons.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Repository.Cache;
using Serilog;
using System.Net;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLoggerSeriLog(configuration);
            services.AddSwagger();
            services.AddMemoryCache();
        }

        private static void AddLoggerSeriLog(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(logger, dispose: true);
            });
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Product Services",
                    Version = "v1",
                    Description = "Get, Insert, Update product",
                    Contact = new OpenApiContact
                    {
                        Name = "Alan Rios",
                        Email = "alanrb92@gmail.com",
                    },
                });
            });
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                                .Get<IExceptionHandlerPathFeature>()?
                                .Error;
                if (exception is null) return;

                var resultError = JsonConvert.SerializeObject(new { Error = exception.Message });
                switch (exception)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadRequestException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        if (((BadRequestException)exception).Errors is not null)
                            resultError = JsonConvert.SerializeObject(new { ((BadRequestException)exception).Errors });
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        resultError = JsonConvert.SerializeObject(new { Error = "An internal error has occurred" });
                        break;
                }

                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "Error: {Message}", resultError);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(resultError);
            }));
        }

        public static IHost ProductCacheInitializer(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var cacheInitializer = services.GetRequiredService<CacheInitializer>();
                cacheInitializer.InitializeCache();
            }
            return host;
        }
    }
}

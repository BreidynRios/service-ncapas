using BusinessLogic.Commons.Interfaces;
using Infrastructure.ServicesClients;
using Infrastructure.ServicesClients.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServicesClientsSettings>(configuration.GetSection("ServicesClients"));
            services.AddTransient<IApiMochaServiceClient, ApiMochaServiceClient>();
            services.AddHttpClient();
        }
    }
}

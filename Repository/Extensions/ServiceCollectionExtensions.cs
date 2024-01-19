using Microsoft.Extensions.DependencyInjection;
using Repository.Cache;
using Repository.Interfaces;
using Repository.Repositories;

namespace Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSingleton<CacheInitializer>();
        }
    }
}

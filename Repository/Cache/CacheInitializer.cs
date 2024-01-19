using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Repository.Cache
{
    public class CacheInitializer
    {
        private readonly IMemoryCache _cache;

        public CacheInitializer(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void InitializeCache()
        {
            var key = "product-status";
            var cachedValue = _cache.Get<List<ProductStatus>>(key);
            if (cachedValue is not null) return;

            var newValue = new List<ProductStatus>
            {
                new() { Status = 0, StatusName = "Inactive" },
                new() { Status = 1, StatusName = "Active" }
            };

            _cache.Set(key, newValue, TimeSpan.FromMinutes(5));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CrackerBarrel.Foundation.Cache
{
    public class CustomMemCache : ICacheManager
    {
        private readonly MemoryCache memoryCache;

        public CustomMemCache()
        {
            memoryCache = MemoryCache.Default;
        }

        public void AddToCache<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (expiration == null)
                expiration = DateTime.Now.AddHours(1.5);
            memoryCache.Add(cacheKey, data, (DateTime)expiration);
        }

        public T GetCachedItem<T>(string cacheKey)
        {
            var data = (T)memoryCache.Get(cacheKey);
            return data;
        }

        public IEnumerable<T> GetCachedList<T>(string cacheKey)
        {
            var data = (IEnumerable<T>)memoryCache.Get(cacheKey);
            return data;
        }

        public void InvalideCache(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
        }

        public void RemoveCache(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
        }

        public void UpdateCache<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (expiration == null)
                expiration = DateTime.Now.AddHours(1.5);
            memoryCache.Add(cacheKey, data, (DateTime)expiration);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackerBarrel.Foundation.Cache
{
    public interface ICacheManager
    {
        void AddToCache<T>(T data, string cacheKey, DateTime? expiration = null);
        IEnumerable<T> GetCachedList<T>(string cacheKey);
        T GetCachedItem<T>(string cacheKey);
        void UpdateCache<T>(T data, string cacheKey, DateTime? expiration = null);
        void InvalideCache(string cacheKey);
        void RemoveCache(string cacheKey);
    }
}

using System;
using System.Collections.Generic;

namespace CrackerBarrel.Foundation.Cache
{
    public interface ICacheManager
    {
        void Add<T>(T data, string cacheKey, DateTime? expiration = null);
        IEnumerable<T> GetList<T>(string cacheKey);
        T Get<T>(string cacheKey);
        void Update<T>(T data, string cacheKey, DateTime? expiration = null);
        void Invalidate(string cacheKey);
        void Delete(string cacheKey);
    }
}
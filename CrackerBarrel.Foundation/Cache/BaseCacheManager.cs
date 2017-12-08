using System;
using System.Collections.Generic;

namespace CrackerBarrel.Foundation.Cache
{
    public abstract class BaseCacheManager : ICacheManager
    {
        protected TimeSpan DefaultLifeSpan = new TimeSpan(1, 0, 0);

        public abstract void Add<T>(T data, string cacheKey, DateTime? expiration = null);
        public abstract void Delete(string cacheKey);
        public abstract T Get<T>(string cacheKey);
        public abstract IEnumerable<T> GetList<T>(string cacheKey);
        public abstract void Invalidate(string cacheKey);
        public abstract void Update<T>(T data, string cacheKey, DateTime? expiration = null);

        protected bool KeyValid(string cacheKey)
        {
            return !string.IsNullOrEmpty(cacheKey);
        }
    }
}
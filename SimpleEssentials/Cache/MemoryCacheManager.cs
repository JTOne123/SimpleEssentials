using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace SimpleEssentials.Cache
{
    public class MemoryCacheManager : BaseCacheManager
    {
        private readonly MemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = MemoryCache.Default;
        }
        public MemoryCacheManager(TimeSpan lifeSpan) : this()
        {
            DefaultLifeSpan = lifeSpan;
        }

        public override void Add<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (!KeyValid(cacheKey))
                return;
            if (expiration == null)
                expiration = DateTime.Now.Add(DefaultLifeSpan);
            _memoryCache.Add(cacheKey, data, (DateTime)expiration);
        }

        public override void Update<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (!KeyValid(cacheKey))
                return;
            Add(data, cacheKey, expiration);
        }

        public override void Delete(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return;
            _memoryCache.Remove(cacheKey);
        }

        public override T Get<T>(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return default(T);
            var data = (T)_memoryCache.Get(cacheKey);
            return data;
        }

        public override IEnumerable<T> GetList<T>(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return default(IEnumerable<T>);
            var data = (IEnumerable<T>)_memoryCache.Get(cacheKey);
            return data;
        }

        public override void Invalidate(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return;
            Delete(cacheKey);
        }
    }
}
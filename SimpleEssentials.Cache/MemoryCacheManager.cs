using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if NETSTANDARD2_0
using Microsoft.Extensions.Caching.Memory;
#else
using System.Runtime.Caching;
#endif


namespace SimpleEssentials.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
#if NETSTANDARD2_0
        private readonly MemoryCache _memoryCache;
#else
        private readonly MemoryCache _memoryCache;
#endif

        public MemoryCacheManager()
        {
            
#if NETSTANDARD2_0
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
#else
            _memoryCache = MemoryCache.Default;
#endif
        }

        private ICacheObject GetCacheObject(ICacheSettings cacheSettings)
        {
            return GetCacheObject(cacheSettings.Key);
        }

        private ICacheObject GetCacheObject(string key)
        {
            if (!CacheHelper.KeyIsValid(key))
                return null;
            var data = (CacheObject)_memoryCache.Get(key);

            if (data == null)
                return null;

            return data.CreateDateTime.Add(data.LifeSpan) < DateTime.UtcNow ? null : data;
        }

        public void Delete(ICacheSettings cacheSettings)
        {
            Delete(cacheSettings.Key);
        }

        public void Delete(string key)
        {
            if (!CacheHelper.KeyIsValid(key))
                return;
            _memoryCache.Remove(key);
        }

        public T Get<T>(ICacheSettings cacheSettings)
        {
            return cacheSettings == null ? default(T) : Get<T>(cacheSettings.Key);
        }

        public T Get<T>(ICacheSettings cacheSettings, Func<T> methodToGetData)
        {
            
            return cacheSettings == null ? default(T) : Get<T>(cacheSettings.Key, methodToGetData, cacheSettings.LifeSpan);
        }

        public T Get<T>(string key)
        {
            if (!CacheHelper.KeyIsValid(key)) return default(T);

            var cacheObj = GetCacheObject(key);
            if (cacheObj == null)
                return default(T);

            if (cacheObj.Data == null)
                return default(T);

            return (T)cacheObj.Data;
        }

        public T Get<T>(string key, Func<T> methodToGetData, TimeSpan? lifeSpan = null)
        {
            if (!CacheHelper.KeyIsValid(key)) return default(T);

            var cacheObj = GetCacheObject(key);
            if (cacheObj?.Data != null) return (T) cacheObj.Data;

            var newData = methodToGetData();
            Insert(newData, key, lifeSpan);

            return (T) newData;
        }

        public void Insert<T>(T data, ICacheSettings cacheSettings)
        {
            if (cacheSettings == null || !CacheHelper.KeyIsValid(cacheSettings.Key)) return;

            Delete(cacheSettings);
            var dataToInsert = CacheHelper.GenerateCacheObject(data, cacheSettings);
            _memoryCache.Set(cacheSettings.Key, dataToInsert, CacheHelper.GetExpirationDate(dataToInsert.LifeSpan));
        }

        public void Insert<T>(T data, string key, TimeSpan? lifeSpan = null)
        {
            if (!CacheHelper.KeyIsValid(key)) return;

            Delete(key);
            var dataToInsert = CacheHelper.GenerateCacheObject(data, key, lifeSpan);
            _memoryCache.Set(key, dataToInsert, CacheHelper.GetExpirationDate(dataToInsert.LifeSpan));
        }

        public void Update<T>(T data, ICacheSettings cacheSettings)
        {
            if (!CacheHelper.KeyIsValid(cacheSettings.Key))
                return;
            Insert(data, cacheSettings);
        }

        public void Update<T>(T data, string key, TimeSpan? lifeSpan = null)
        {
            if (!CacheHelper.KeyIsValid(key))
                return;
            Insert(data, key, lifeSpan);
        }


    }
}

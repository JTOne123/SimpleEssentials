using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Dapper.Contrib.Extensions;

namespace SimpleEssentials.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly MemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = MemoryCache.Default;
        }

        public void Delete(CacheSettings cacheSettings)
        {
            if (!CacheHelper.IsKeyValid(cacheSettings.Key))
                return;
            _memoryCache.Remove(cacheSettings.Key);
        }

        public void DeleteHash(CacheSettings cacheSettings)
        {
            var keys = (IEnumerable<string>)_memoryCache.Get(cacheSettings.Key + "_KEYS");
            if (keys == null)
                return;

            foreach (var key in keys)
            {
                DeleteSingleHash(cacheSettings, key);
            }

            _memoryCache.Remove(cacheSettings.Key + "_KEYS");
        }

        public void DeleteSingleHash(CacheSettings cacheSettings, string fieldKey)
        {
            _memoryCache.Remove(cacheSettings.Key + ":" + fieldKey);
        }

        public T Get<T>(CacheSettings cacheSettings)
        {
            if (!CacheHelper.IsKeyValid(cacheSettings.Key))
                return default(T);
            return (T)_memoryCache.Get(cacheSettings.Key);
        }

        public IEnumerable<T> GetHash<T>(CacheSettings cacheSettings)
        {
            var keys = (IEnumerable<string>)_memoryCache.Get(cacheSettings.Key + "_KEYS");
            return keys?.Select(x => GetSingleHash<T>(cacheSettings, x));
        }

        public IEnumerable<T> GetList<T>(CacheSettings cacheSettings)
        {
            if (!CacheHelper.IsKeyValid(cacheSettings.Key))
                return default(IEnumerable<T>);
            return (IEnumerable<T>)_memoryCache.Get(cacheSettings.Key);
        }

        public T GetSingleHash<T>(CacheSettings cacheSettings, string fieldKey)
        {
            return (T)_memoryCache.Get(cacheSettings.Key + ":" + fieldKey);
        }

        public void Insert<T>(T data, CacheSettings cacheSettings)
        {
            if (!CacheHelper.IsKeyValid(cacheSettings.Key))
                return;
            
            _memoryCache.Add(cacheSettings.Key, data, CacheHelper.GetExpirationDate(cacheSettings.LifeSpan));
        }

        public void InsertHash<T>(IEnumerable<T> data, CacheSettings cacheSettings)
        {
            var dataList = data as IList<T> ?? data.ToList();
            var keys = dataList.Select(x => CacheHelper.GetObjectFieldKey(x));
            _memoryCache.Add(cacheSettings.Key + "_KEYS", keys, CacheHelper.GetExpirationDate(cacheSettings.LifeSpan));

            foreach (var item in dataList)
            {
                InsertSingleHash(item, cacheSettings);
            }
        }

        public void InsertSingleHash<T>(T data, CacheSettings cacheSettings)
        {
            var key = CacheHelper.GetObjectFieldKey(data);
            InsertSingleHash(data, cacheSettings, key);
        }

        public void InsertSingleHash<T>(T data, CacheSettings cacheSettings, string fieldKey)
        {
            if (string.IsNullOrEmpty(fieldKey))
                return;
            _memoryCache.Add(cacheSettings.Key + ":" + fieldKey, data, CacheHelper.GetExpirationDate(cacheSettings.LifeSpan));
        }

        

        public void Update<T>(T data, CacheSettings cacheSettings)
        {
            if (!CacheHelper.IsKeyValid(cacheSettings.Key))
                return;
            Insert(data, cacheSettings);
        }

        public void UpdateHash<T>(IEnumerable<T> data, CacheSettings cacheSettings)
        {
            DeleteHash(cacheSettings);
            InsertHash(data, cacheSettings);
        }

        public void UpdateSingleHash<T>(T data, CacheSettings cacheSettings)
        {
            var key = CacheHelper.GetObjectFieldKey(data);
            InsertSingleHash(data, cacheSettings, key);
        }
    }
}
using System;
using System.CodeDom;
using System.Collections;
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

        public CacheObject Get(CacheSettings cacheSettings)
        {
            if (!CacheHelper.IsKeyValid(cacheSettings.Key))
                return null;
            return (CacheObject)_memoryCache.Get(cacheSettings.Key);
        }

        public T GetData<T>(CacheSettings cacheSettings, string fieldKey = null)
        {
            if (cacheSettings == null || !CacheHelper.IsKeyValid(cacheSettings.Key)) return default(T);

            var cacheObj = Get(cacheSettings);
            if (cacheObj == null)
                return default(T);

            if (cacheObj.Data == null)
                return default(T);

            return (T)cacheObj.Data;
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
            if (cacheSettings == null || CacheHelper.IsKeyValid(cacheSettings.Key)) return;
            

            ICacheObject dataToInsert;

            var cacheObj = Get(cacheSettings);
            if (cacheObj != null)
            {
                Delete(cacheSettings);
                if (cacheObj.ObjectType == CacheObjectType.List)
                {
                    var newData = ((IEnumerable<T>)cacheObj.Data).ToList();
                    newData.Add(data);

                    dataToInsert = GenerateCacheObject(newData);
                }
                else
                {
                    dataToInsert = GenerateCacheObject(data);
                }
            }
            else
            {
                dataToInsert = GenerateCacheObject(data);
            }

            _memoryCache.Add(cacheSettings.Key, dataToInsert, CacheHelper.GetExpirationDate(cacheSettings.LifeSpan));
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

        private ICacheObject GenerateCacheObject<T>(T data)
        {
            return new CacheObject()
            {
                Data = data,
                ObjectType = (data is IEnumerable) ? CacheObjectType.List : CacheObjectType.Single
            };
        }
    }
}
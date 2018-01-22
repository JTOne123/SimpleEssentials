using System;
using System.Collections.Generic;

namespace SimpleEssentials.Cache
{
    public interface ICacheManager
    {
        void Insert<T>(T data, CacheSettings cacheSettings);
        void InsertHash<T>(IEnumerable<T> data, CacheSettings cacheSettings);
        void InsertSingleHash<T>(T data, CacheSettings cacheSettings);
        void InsertSingleHash<T>(T data, CacheSettings cacheSettings, string fieldKey);
        void Update<T>(T data, CacheSettings cacheSettings);
        void UpdateHash<T>(IEnumerable<T> data, CacheSettings cacheSettings);
        void UpdateSingleHash<T>(T data, CacheSettings cacheSettings);
        void Delete(CacheSettings cacheSettings);
        void DeleteHash(CacheSettings cacheSettings);
        void DeleteSingleHash(CacheSettings cacheSettings, string fieldKey);
        T Get<T>(CacheSettings cacheSettings);
        T GetSingleHash<T>(CacheSettings cacheSettings, string fieldKey);
        IEnumerable<T> GetList<T>(CacheSettings cacheSettings);
        IEnumerable<T> GetHash<T>(CacheSettings cacheSettings);
    }
}
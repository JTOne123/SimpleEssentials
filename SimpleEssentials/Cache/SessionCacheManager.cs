using System;
using System.Collections.Generic;
using System.Web;

namespace SimpleEssentials.Cache
{
    public class SessionCacheManager : ICacheManager
    {
        public void Delete(CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void DeleteHash(CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void DeleteSingleHash(CacheSettings cacheSettings, string fieldKey)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetHash<T>(CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList<T>(CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public T GetSingleHash<T>(CacheSettings cacheSettings, string fieldKey)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void InsertHash<T>(IEnumerable<T> data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void InsertHash<T>(T data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void InsertSingleHash<T>(T data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void InsertSingleHash<T>(T data, CacheSettings cacheSettings, string fieldKey)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void UpdateHash<T>(IEnumerable<T> data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }

        public void UpdateSingleHash<T>(T data, CacheSettings cacheSettings)
        {
            throw new NotImplementedException();
        }
    }
}
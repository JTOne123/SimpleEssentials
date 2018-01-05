using System;
using System.Collections.Generic;

namespace SimpleEssentials.Cache
{
    public abstract class BaseCacheManager : ICacheManager
    {
        protected TimeSpan DefaultLifeSpan = new TimeSpan(1, 0, 0);

        public abstract void Add<T>(T data, string cacheKey, DateTime? expiration = null);

        public virtual void AddHash<T>(IEnumerable<T> data, string cacheKey, string fieldKey,
            DateTime? expiration = null)
        {
            throw new NotImplementedException();
        }
        public abstract void Delete(string cacheKey);

        public virtual void DeleteHash(string cacheKey, string fieldKey)
        {
            throw new NotImplementedException();
        }

        public virtual T GetHash<T>(string cacheKey, string fieldKey)
        {
            throw new NotImplementedException();
        }
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
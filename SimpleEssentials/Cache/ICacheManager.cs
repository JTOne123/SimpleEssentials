using System;
using System.Collections.Generic;

namespace SimpleEssentials.Cache
{
    public interface ICacheManager
    {
        void Add<T>(T data, string cacheKey, DateTime? expiration = null);
        void AddHash<T>(IEnumerable<T> data, string cacheKey, DateTime? expiration = null);
        void AddHash<T>(T data, string cacheKey, DateTime? expiration = null);
        T GetHash<T>(string cacheKey, string fieldKey);
        IEnumerable<T> GetList<T>(string cacheKey);
        T Get<T>(string cacheKey);
        void Update<T>(T data, string cacheKey, DateTime? expiration = null);
        void Invalidate(string cacheKey);
        void Delete(string cacheKey);
        void DeleteHash(string cacheKey, string fieldKey);
    }
}
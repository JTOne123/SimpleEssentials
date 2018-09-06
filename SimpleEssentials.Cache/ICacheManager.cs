using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.Cache
{
    public interface ICacheManager
    {
        void Insert<T>(T data, ICacheSettings cacheSettings);
        void Insert<T>(T data, string key, TimeSpan? lifeSpan);
        void Update<T>(T data, ICacheSettings cacheSettings);
        void Update<T>(T data, string key, TimeSpan? lifeSpan);
        void Delete(ICacheSettings cacheSettings);
        void Delete(string key);
        T Get<T>(ICacheSettings cacheSettings);
        T Get<T>(ICacheSettings cacheSettings, Func<T> methodToGetData);
        T Get<T>(string key);
        T Get<T>(string key, Func<T> methodToGetData, TimeSpan? lifeSpan);
    }
}

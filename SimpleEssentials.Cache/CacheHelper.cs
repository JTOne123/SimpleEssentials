using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.Cache
{
    internal static class CacheHelper
    {
        public static bool KeyIsValid(string cacheKey)
        {
            return !string.IsNullOrEmpty(cacheKey);
        }

        public static DateTime GetExpirationDate(TimeSpan lifeSpan)
        {
            return DateTime.UtcNow.Add(lifeSpan);
        }

        public static ICacheObject GenerateCacheObject<T>(T data, ICacheSettings cacheSettings)
        {
            return new CacheObject()
            {
                Data = data,
                LifeSpan = cacheSettings.LifeSpan ?? new TimeSpan(30, 0, 0, 0)
            };
        }

        public static ICacheObject GenerateCacheObject<T>(T data, string key, TimeSpan? lifeSpan = null)
        {
            return new CacheObject()
            {
                Data = data,
                LifeSpan = lifeSpan ?? new TimeSpan(30, 0, 0, 0)
            };
        }
    }
}

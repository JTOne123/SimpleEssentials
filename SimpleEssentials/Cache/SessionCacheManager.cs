using System;
using System.Collections.Generic;
using System.Web;

namespace SimpleEssentials.Cache
{
    public class SessionCacheManager : BaseCacheManager
    {

        public SessionCacheManager()
        {
        }

        public SessionCacheManager(TimeSpan lifeSpan) : this()
        {
            DefaultLifeSpan = lifeSpan;
        }

        private static bool HasExpired<T>(SessionObject<T> sessionObj)
        {
            if (sessionObj.Expiration == null)
                return false;

            return DateTime.Now > sessionObj.Expiration;
        }

        public override void Add<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (!KeyValid(cacheKey))
                return;
            var sessionObj = new SessionObject<T>
            {
                Data = data,
                Expiration = expiration
            };
            HttpContext.Current?.Session.Add(cacheKey, sessionObj);
        }

        public override void Delete(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return;
            HttpContext.Current?.Session.Remove(cacheKey);
        }

        public override T Get<T>(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return default(T);
            var sessionObj = (SessionObject<T>)HttpContext.Current?.Session[cacheKey];
            if (sessionObj == null || HasExpired(sessionObj))
                return default(T);
            return sessionObj.Data;
        }

        public override IEnumerable<T> GetList<T>(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return default(IEnumerable<T>);
            var sessionObj = (SessionObject<IEnumerable<T>>)HttpContext.Current?.Session[cacheKey];
            if (sessionObj == null || HasExpired(sessionObj))
                return null;
            return sessionObj.Data;
        }

        public override void Invalidate(string cacheKey)
        {
            if (!KeyValid(cacheKey))
                return;
            Delete(cacheKey);
        }

        public override void Update<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (!KeyValid(cacheKey))
                return;
            Add(data, cacheKey, expiration);
        }
    }
}
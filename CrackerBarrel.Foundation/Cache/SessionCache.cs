using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CrackerBarrel.Foundation.Cache
{
    public class SessionCache : ICacheManager
    {
        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static bool HasExpired<T>(SessionObject<T> sessionObj)
        {
            if (sessionObj.Expiration == null)
                return false;
            if (DateTime.Now > sessionObj.Expiration)
                return true;

            return false;
        }

        public void AddToCache<T>(T data, string cacheKey, DateTime? expiration)
        {
            var sessionObj = new SessionObject<T>();
            sessionObj.Data = data;
            sessionObj.Expiration = expiration;
            HttpContext.Current.Session.Add(cacheKey, sessionObj);
        }

        public IEnumerable<T> GetCachedList<T>(string cacheKey)
        {
            var sessionObj = (SessionObject<IEnumerable<T>>)HttpContext.Current.Session[cacheKey];
            if (sessionObj == null || HasExpired(sessionObj))
                return null;
            return sessionObj.Data;
        }

        public T GetCachedItem<T>(string cacheKey)
        {
            var sessionObj = (SessionObject<T>)HttpContext.Current.Session[cacheKey];
            if (sessionObj == null || HasExpired(sessionObj))
                return default(T);
            return sessionObj.Data;
        }

        public void UpdateCache<T>(T data, string cacheKey, DateTime? expiration)
        {
            var sessionObj = new SessionObject<T>();
            sessionObj.Data = data;
            sessionObj.Expiration = expiration;
            HttpContext.Current.Session[cacheKey] = sessionObj;
        }

        public void InvalideCache(string cacheKey)
        {
            Remove(cacheKey);
        }

        public void RemoveCache(string cacheKey)
        {
            Remove(cacheKey);
        }


        //TODO: Function to get and if key doesnt exists make it call a callback function to populate the cache
    }
}

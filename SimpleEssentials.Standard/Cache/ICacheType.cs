using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Cache
{
    public interface ICacheType
    {
        void Insert<T>(T data, string cacheKey, TimeSpan? lifeSpan = null);
        void InsertHash<T>(IEnumerable<T> data, string cacheKey, TimeSpan? lifeSpan = null);
        void InsertSingleHash<T>(T data, string cacheKey, TimeSpan? lifeSpan = null);
        void Update<T>(T data, string cacheKey, TimeSpan? lifeSpan = null);
        void UpdateHash<T>(IEnumerable<T> data, string cacheKey, TimeSpan? lifeSpan = null);
        void UpdateSingleHash<T>(T data, string cacheKey, TimeSpan? lifeSpan = null);
        void Delete(string cacheKey);
        void DeleteHash(string cacheKey);
        void DeleteSingleHash(string cacheKey, string fieldKey);
        T Get<T>(string cacheKey);
        T GetSingleHash<T>(string cacheKey, string fieldKey);
        IEnumerable<T> GetList<T>(string cacheKey);
        IEnumerable<T> GetHash<T>(string cacheKey);

    }
}

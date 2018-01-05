using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.DataProvider
{
    public interface IDbProvider : IDataProvider
    {
        bool Add<T>(T obj, string cacheKey = null, DateTime? lifeTime = null, bool invalidateCache = false) where T : class, new();
        int AddAndReturnId<T>(string sql, T obj, string cacheKey = null, DateTime? lifeTime = null, bool invalidateCache = false) where T : class, new();
        int AddList<T>(IEnumerable<T> obj, string sql, string cacheKey = null, DateTime? lifeTime = null, bool invalidateCache = false) where T : class, new();
        void BulkInsert<T>(IEnumerable<T> obj, string tableName, string cacheKey = null, DateTime? lifeTime = null, bool invalidateCache = false) where T : class, new();
        T Get<T>(object id, string cacheKey = null, DateTime? lifeTime = null) where T : class, new();
        IEnumerable<T> GetByType<T>(string cacheKey = null, DateTime? lifeTime = null) where T : class, new();
        IEnumerable<T> GetByParameters<T>(string sql, object param, bool forceCache = false, string cacheKey = "", DateTime? lifeTime = null);
        IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "", string cacheKey = "", DateTime? lifeTime = null);
        IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "", string cacheKey = "", DateTime? lifeTime = null);
        int Execute(string sql, object param, string cacheKey = null, DateTime? lifeTime = null, bool invalidate = false);
        int ExecuteScalar(string sql, object param, string cacheKey = null, DateTime? lifeTime = null, bool invalidate = false);
        bool Delete<T>(T obj, string cacheKey = null) where T : class, new();
        bool Update<T>(T obj, string cacheKey = null, DateTime? lifeTime = null) where T : class, new();
    }
}

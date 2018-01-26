using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEssentials.Cache;

namespace SimpleEssentials.DataProvider
{
    public interface IDbProvider : IDataProvider
    {
        bool Insert<T>(T data, CacheSettings cacheSettings = null) where T : class, new();
        int InsertList<T>(IEnumerable<T> data, CacheSettings cacheSettings = null) where T : class, new();
        [Obsolete("Please use InsertList without the sql string instead")]
        int InsertList<T>(IEnumerable<T> data, string sql, CacheSettings cacheSettings = null) where T : class, new();
        int InsertAndReturnId<T>( T data, CacheSettings cacheSettings = null) where T : class, new();
        [Obsolete("Please use InsertAndReturnId without the sql string instead")]
        int InsertAndReturnId<T>(string sql, T data, CacheSettings cacheSettings = null) where T : class, new();
        void BulkInsert<T>(IEnumerable<T> data, string tableName, CacheSettings cacheSettings = null) where T : class, new();
        bool Update<T>(T data, CacheSettings cacheSettings = null) where T : class, new();
        bool Delete<T>(T data, CacheSettings cacheSettings = null, string fieldKey = null) where T : class, new();
        int Execute(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false);
        int ExecuteScalar(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false);
        T Get<T>(object id, CacheSettings cacheSettings = null) where T : class, new();
        //T Get<T>(object id, CacheSettings cacheSettings, string fieldKey) where T : class, new();
        IEnumerable<T> GetByType<T>(CacheSettings cacheSettings = null) where T : class, new();
        IEnumerable<T> GetByParameters<T>(string sql, object param, CacheSettings cacheSettings = null);
        IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null);
        IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null);
        IEnumerable<T> GetMultiMap<T, T2, T3, T4>(string sql, Func<T, T2, T3, T4, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null);
        IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5>(string sql, Func<T, T2, T3, T4, T5, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null);
        IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5, T6>(string sql, Func<T, T2, T3, T4, T5, T6, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null);
    }
}

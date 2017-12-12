using System;
using System.Collections.Generic;

namespace SimpleEssentials.DataStore
{
    public interface IDbStore : IDataStore
    {
        bool Add<T>(T obj) where T : class, new();
        int AddAndReturnId<T>(string sql, T obj) where T : class, new();
        int AddList<T>(IEnumerable<T> obj, string sql) where T : class, new();
        T Get<T>(object id) where T : class, new();
        IEnumerable<T> GetByType<T>() where T : class, new();
        IEnumerable<T> GetByParameters<T>(string sql, object param);
        IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "");
        IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "");
        int Execute(string sql, object param);
        bool Delete<T>(T obj) where T : class, new();
        bool Update<T>(T obj) where T : class, new();
    }
}
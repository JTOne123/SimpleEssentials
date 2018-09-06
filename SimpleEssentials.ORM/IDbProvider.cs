using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleEssentials.ORM
{
    public interface IDbProvider
    {
        bool Insert<T>(T obj) where T : class, new();
        int InsertAndReturnId<T>(T obj) where T : class, new();
        //int AddList<T>(IEnumerable<T> obj, string sql) where T : class, new();
        //void BulkInsert<T>(IEnumerable<T> obj, string tableName) where T : class;
        IEnumerable<T> Get<T>() where T : class, new();
        T Get<T>(object id) where T : class, new();
        IEnumerable<T> Get<T>(Expression<Func<T, bool>> expression) where T : class, new();
        //IEnumerable<T> GetByType<T>() where T : class, new();
        IEnumerable<T> GetByParameters<T>(string sql, object param);
        IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "");
        IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "");
        IEnumerable<T> GetMultiMap<T, T2, T3, T4>(string sql, Func<T, T2, T3, T4, T> func, object param = null, string splitOn = "");
        IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5>(string sql, Func<T, T2, T3, T4, T5, T> func, object param = null, string splitOn = "");
        IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5, T6>(string sql, Func<T, T2, T3, T4, T5, T6, T> func, object param = null, string splitOn = "");
        int Execute(string sql, object param);
        int ExecuteScalar(string sql, object param);
        bool Delete<T>(T obj) where T : class, new();
        bool Update<T>(T obj) where T : class, new();
    }
}

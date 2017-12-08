using System;
using System.Collections.Generic;
using CrackerBarrel.Foundation.Cache;
using CrackerBarrel.Foundation.DataStore;

namespace CrackerBarrel.Foundation.DataProvider
{
    public class DbDataProvider : IDbProvider
    {
        private readonly IDbStore _dbStore;
        private readonly ICacheManager _cacheManager;

        public DbDataProvider(IDbStore dataStore, ICacheManager cacheManager)
        {
            _dbStore = dataStore;
            _cacheManager = cacheManager;
        }

        public bool Add<T>(T obj, string cacheKey = null, DateTime? lifeTime = null, bool invalidateCache = false) where T : class, new()
        {

            if (invalidateCache)
                _cacheManager?.Invalidate(cacheKey);
            else
                _cacheManager?.Add(obj, cacheKey);
            return _dbStore.Add(obj);
        }

        public int AddList<T>(IEnumerable<T> obj, string sql, string cacheKey = null, DateTime? lifeTime = null, bool invalidateCache = false) where T : class, new()
        {
            if (invalidateCache)
                _cacheManager?.Invalidate(cacheKey);
            else
                _cacheManager?.Add(obj, cacheKey);
            return _dbStore.AddList(obj, sql);
        }

        public T Get<T>(object id, string cacheKey = null, DateTime? lifeTime = null) where T : class, new()
        {
            var results = _cacheManager?.Get<T>(cacheKey);
            if (results == null)
            {
                results = _dbStore.Get<T>(id);
                _cacheManager?.Add(results, cacheKey, lifeTime);
            }
            return results;
        }

        public IEnumerable<T> GetByParameters<T>(string sql, object param, bool forceCache = false, string cacheKey = null, DateTime? lifeTime = null)
        {
            var results =_cacheManager?.GetList<T>(cacheKey);
            if (results == null || forceCache)
            {
                results = _dbStore.GetByParameters<T>(sql, param);
                _cacheManager?.Add(results, cacheKey, lifeTime);
            }
            return results;
        }

        public IEnumerable<T> GetByType<T>(string cacheKey = null, DateTime? lifeTime = null) where T : class, new()
        {
            var results = _cacheManager?.GetList<T>(cacheKey);
            if (results == null)
            {
                results = _dbStore.GetByType<T>();
                _cacheManager?.Add(results, cacheKey, lifeTime);
            }
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string cacheKey = "", DateTime? lifeTime = null)
        {
            var results = _cacheManager?.GetList<T>(cacheKey);
            if (results == null)
            {
                results = _dbStore.GetMultiMap<T, T2>(sql, func, param);
                _cacheManager?.Add(results, cacheKey, lifeTime);
            }
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string cacheKey = "", DateTime? lifeTime = null)
        {
            var results = _cacheManager?.GetList<T>(cacheKey);
            if (results == null)
            {
                results = _dbStore.GetMultiMap<T, T2, T3>(sql, func, param);
                _cacheManager?.Add(results, cacheKey, lifeTime);
            }
            return results;
        }

        public bool Delete<T>(T obj, string cacheKey = null) where T : class, new()
        {
            _cacheManager?.Delete(cacheKey);
            return _dbStore.Delete(obj);
        }

        public int Execute(string sql, object param, string cacheKey = null, DateTime? lifeTime = null)
        {
            _cacheManager?.Invalidate(cacheKey);
            return _dbStore.Execute(sql, param);
        }

        public bool Update<T>(T obj, string cacheKey = null, DateTime? lifeTime = null) where T : class, new()
        {
            _cacheManager?.Update(obj, cacheKey, lifeTime);
            return _dbStore.Update(obj);
        }

        
    }
}
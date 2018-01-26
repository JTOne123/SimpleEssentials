using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleEssentials.Cache;
using SimpleEssentials.DataStore;

namespace SimpleEssentials.DataProvider
{
    public class DbDataProvider : IDbProvider
    {
        private readonly IDataStore _dataStore;
        private readonly ICacheManager _cacheManager;

        public DbDataProvider()
        {
            _dataStore = Factory.Container.GetInstance<IDataStore>();
            _cacheManager = Factory.Container.GetInstance<ICacheManager>();
        }

        public DbDataProvider(IDataStore dataStore, ICacheManager cacheManager)
        {
            _dataStore = dataStore;
            _cacheManager = cacheManager ?? Factory.Container.GetInstance<ICacheManager>();
        }

        public void BulkInsert<T>(IEnumerable<T> data, string tableName, CacheSettings cacheSettings = null) where T : class, new()
        {
            _cacheManager?.Insert(data, cacheSettings);
            _dataStore.BulkInsert(data, tableName);
        }

        public bool Delete<T>(T data, CacheSettings cacheSettings = null, string fieldKey = null) where T : class, new()
        {
            DeleteFromCache(cacheSettings, fieldKey);
           return _dataStore.Delete(data);
        }

        public int Execute(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false)
        {
            if (invalidateCache)
                DeleteFromCache(cacheSettings);
            
            var results = _cacheManager?.GetData<int>(cacheSettings);
            if (results != null && results != 0) return results.Value;

            results = _dataStore.Execute(sql, param);
            _cacheManager?.Insert(results, cacheSettings);
            return results.Value;
        }

        public int ExecuteScalar(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false)
        {
            if (invalidateCache)
                DeleteFromCache(cacheSettings);
            
            var results = _cacheManager?.GetData<int>(cacheSettings);
            if (results != null && results != 0) return results.Value;

            results = _dataStore.ExecuteScalar(sql, param);
            _cacheManager?.Insert(results, cacheSettings);
            return results.Value;
        }

        public T Get<T>(object id, CacheSettings cacheSettings = null) where T : class, new()
        {
            var results = _cacheManager?.GetData<T>(cacheSettings, id.ToString());
            if (results != null) return results;

            results = _dataStore.Get<T>(id);
            _cacheManager?.Insert(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetByParameters<T>(string sql, object param, CacheSettings cacheSettings = null)
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetByParameters<T>(sql, param);
            _cacheManager?.Insert(results, cacheSettings);

            return results;
        }

        public IEnumerable<T> GetByType<T>(CacheSettings cacheSettings = null) where T : class, new()
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetByType<T>();
            _cacheManager?.Insert(results, cacheSettings);

            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            _cacheManager?.Insert(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            _cacheManager?.Insert(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4>(string sql, Func<T, T2, T3, T4, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            _cacheManager?.Insert(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5>(string sql, Func<T, T2, T3, T4, T5, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            _cacheManager?.Insert(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5, T6>(string sql, Func<T, T2, T3, T4, T5, T6, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = _cacheManager?.GetData<IEnumerable<T>>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            _cacheManager?.Insert(results, cacheSettings);
            return results;
        }

        public bool Insert<T>(T data, CacheSettings cacheSettings = null) where T : class, new()
        {
            _cacheManager?.Insert(data, cacheSettings);
            return _dataStore.Add(data);
        }


        public int InsertAndReturnId<T>(string sql, T data, CacheSettings cacheSettings = null) where T : class, new()
        {
            _cacheManager?.Insert(data, cacheSettings);
            return _dataStore.AddAndReturnId(sql, data);
        }

        public int InsertList<T>(IEnumerable<T> data, string sql, CacheSettings cacheSettings = null) where T : class, new()
        {
            _cacheManager?.Insert(data, cacheSettings);
            return _dataStore.AddList(data, sql);
        }

        public bool Update<T>(T data, CacheSettings cacheSettings = null) where T : class, new()
        {
            UpdateCache(data, cacheSettings);
            return _dataStore.Update(data);
        }

        private void DeleteFromCache(CacheSettings cacheSettings, string fieldKey = null)
        {
            if (cacheSettings == null || _cacheManager == null) return;

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    _cacheManager?.Delete(cacheSettings);
                    break;
                case CacheStorage.Hashed:
                    if(!string.IsNullOrEmpty(fieldKey))
                        _cacheManager?.DeleteSingleHash(cacheSettings, fieldKey);
                    else
                        _cacheManager?.DeleteHash(cacheSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }

        private void UpdateCache<T>(T data, CacheSettings cacheSettings)
        {
            if (cacheSettings == null) return;

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    _cacheManager?.Update(data, cacheSettings);
                    break;
                case CacheStorage.Hashed:
                    if (data is IEnumerable<T> enumerable)
                        _cacheManager?.UpdateHash(enumerable, cacheSettings);
                    else
                        _cacheManager?.UpdateSingleHash(data, cacheSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }
    }
}
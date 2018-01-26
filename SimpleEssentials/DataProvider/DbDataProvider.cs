using System;
using System.Collections.Generic;
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
            _cacheManager = cacheManager;
        }

        public void BulkInsert<T>(IEnumerable<T> data, string tableName, CacheSettings cacheSettings = null) where T : class, new()
        {
            var cacheData = GetListFromCache<T>(cacheSettings);
            if(cacheData != null)
                DeleteFromCache(cacheSettings);
            else
                InsertListIntoCache(data, cacheSettings);


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

            var results = GetFromCache<int>(cacheSettings, sql);
            if (results != 0) return results;

            results = _dataStore.Execute(sql, param);
            InsertIntoCache(results, cacheSettings);
            return results;
        }

        public int ExecuteScalar(string sql, object param, CacheSettings cacheSettings = null, bool invalidateCache = false)
        {
            if (invalidateCache)
                DeleteFromCache(cacheSettings);

            var results = GetFromCache<int>(cacheSettings, sql);
            if (results != 0) return results;

            results = _dataStore.ExecuteScalar(sql, param);
            InsertIntoCache(results, cacheSettings);
            return results;
        }

        public T Get<T>(object id, CacheSettings cacheSettings = null) where T : class, new()
        {
            var results = GetFromCache<T>(cacheSettings, id.ToString());
            if (results != null) return results;

            results = _dataStore.Get<T>(id);
            InsertIntoCache(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetByParameters<T>(string sql, object param, CacheSettings cacheSettings = null)
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetByParameters<T>(sql, param);
            InsertListIntoCache(results, cacheSettings);

            return results;
        }

        public IEnumerable<T> GetByType<T>(CacheSettings cacheSettings = null) where T : class, new()
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetByType<T>();
            InsertListIntoCache(results, cacheSettings);

            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            InsertListIntoCache(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            InsertListIntoCache(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4>(string sql, Func<T, T2, T3, T4, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            InsertListIntoCache(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5>(string sql, Func<T, T2, T3, T4, T5, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            InsertListIntoCache(results, cacheSettings);
            return results;
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5, T6>(string sql, Func<T, T2, T3, T4, T5, T6, T> func, object param = null, string splitOn = "", CacheSettings cacheSettings = null)
        {
            var results = GetListFromCache<T>(cacheSettings);
            if (results != null) return results;

            results = _dataStore.GetMultiMap(sql, func, param, splitOn);
            InsertListIntoCache(results, cacheSettings);
            return results;
        }

        public bool Insert<T>(T data, CacheSettings cacheSettings = null) where T : class, new()
        {
            var cacheData = GetFromCache<T>(cacheSettings);
            if (cacheData != null)
                DeleteFromCache(cacheSettings);
            else
                InsertIntoCache(data, cacheSettings);
            return _dataStore.Add(data);
        }

        public int InsertAndReturnId<T>(string sql, T data, CacheSettings cacheSettings = null) where T : class, new()
        {
            var cacheData = GetFromCache<T>(cacheSettings);
            if (cacheData != null)
                DeleteFromCache(cacheSettings);
            else
                InsertIntoCache(data, cacheSettings);
            return _dataStore.AddAndReturnId(sql, data);
        }

        public int InsertList<T>(IEnumerable<T> data, string sql, CacheSettings cacheSettings = null) where T : class, new()
        {
            InsertIntoCache(data, cacheSettings);
            return _dataStore.AddList(data, sql);
        }

        public bool Update<T>(T data, CacheSettings cacheSettings = null) where T : class, new()
        {
            UpdateCache(data, cacheSettings);
            return _dataStore.Update(data);
        }

        private void InsertIntoCache<T>(T data, CacheSettings cacheSettings)
        {
            if (cacheSettings == null) return;

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    _cacheManager?.Insert(data, cacheSettings);
                    break;
                case CacheStorage.Hashed:
                    _cacheManager.InsertSingleHash(data, cacheSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }

        private void InsertListIntoCache<T>(IEnumerable<T> data, CacheSettings cacheSettings)
        {
            if (cacheSettings == null) return;

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    _cacheManager?.Insert(data, cacheSettings);
                    break;
                case CacheStorage.Hashed:
                    _cacheManager.InsertHash(data, cacheSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }

        private void DeleteFromCache(CacheSettings cacheSettings, string fieldKey = null)
        {
            if (cacheSettings == null) return;

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    _cacheManager?.Delete(cacheSettings);
                    break;
                case CacheStorage.Hashed:
                    if(!string.IsNullOrEmpty(fieldKey))
                        _cacheManager.DeleteSingleHash(cacheSettings, fieldKey);
                    else
                        _cacheManager.DeleteHash(cacheSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }

        private T GetFromCache<T>(CacheSettings cacheSettings, string fieldKey = null)
        {
            if (cacheSettings == null) return default(T);

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    return _cacheManager.Get<T>(cacheSettings);
                case CacheStorage.Hashed:
                    return _cacheManager.GetSingleHash<T>(cacheSettings, fieldKey);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }

        private IEnumerable<T> GetListFromCache<T>(CacheSettings cacheSettings)
        {
            if (cacheSettings == null) return default(IEnumerable<T>);

            switch (cacheSettings.StorageType)
            {
                case CacheStorage.Normal:
                    return _cacheManager.GetList<T>(cacheSettings);
                case CacheStorage.Hashed:
                    return _cacheManager.GetHash<T>(cacheSettings);
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
                    _cacheManager.Update(data, cacheSettings);
                    break;
                case CacheStorage.Hashed:
                    if (data is IEnumerable<T> enumerable)
                        _cacheManager.UpdateHash(enumerable, cacheSettings);
                    else
                        _cacheManager.UpdateSingleHash(data, cacheSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheSettings.StorageType), cacheSettings.StorageType, null);
            }
        }


    }
}
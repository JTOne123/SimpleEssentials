using CrackerBarrel.Foundation.Cache;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackerBarrel.Foundation.Repository.Base
{
    public abstract class CrudRepository
    {
        protected string ConnectionName;
        protected ICacheManager CacheManager;

        private string ConnectionString { get; set; }

        public CrudRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public CrudRepository(ICacheManager cacheManager, string connectionString)
        {
            CacheManager = cacheManager;
            ConnectionString = connectionString;
        }

        //public BaseRepository(ICacheManager cacheManager = null, string connectionName = "Portal")
        //{
        //    CacheManager = cacheManager;
        //    ConnectionName = connectionName;
        //}

        public virtual bool Insert<T>(T obj, string cacheKey = "", DateTime? expiration = null) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    long rowsAffected = connection.Insert(obj);
                    if (rowsAffected == 0)
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                //TODO: log this error
            }
            InvalidateCache(cacheKey);
            return true;
        }

        public virtual T GetById<T>(int id, string cacheKey = "", DateTime? expiration = null) where T : class, new()
        {
            var data = GetCachedItem<T>(cacheKey);
            if(data == null)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    data = connection.Get<T>(id);
                    AddDataToCache<T>(data, cacheKey, expiration);
                }
            }
            return data;
            
        }

        public virtual T GetById<T>(string id, string cacheKey = "", DateTime? expiration = null) where T : class, new()
        {
            var data = GetCachedItem<T>(cacheKey);
            if(data == null)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    data = connection.Get<T>(id);
                    AddDataToCache<T>(data, cacheKey, expiration);
                }
            }
            return data;
            
        }

        public virtual IEnumerable<T> GetAll<T>(string cacheKey = "", DateTime? expiration = null) where T : class, new()
        {
            var data = GetCachedList<T>(cacheKey);
            if(data == null)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    data = connection.GetAll<T>().ToList();
                    AddDataToCache<IEnumerable<T>>(data, cacheKey, expiration);
                }
            }
            return data;
        }

        public virtual IEnumerable<T> GetAllByEmpId<T>(string empId, string sql, string cacheKey = "", DateTime? expiration = null)
        {
            var data = GetCachedList<T>(cacheKey);
            if(data == null)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    data = connection.Query<T>(sql, new { employeeId = empId.PadLeft(9) }).ToList();
                    AddDataToCache<IEnumerable<T>>(data, cacheKey, expiration);
                }
            }
            
            return data;
        }

        public virtual IEnumerable<T> GetAllByParameters<T>(string sql, object param, string cacheKey = "", DateTime? expiration = null)
        {
            var data = GetCachedList<T>(cacheKey);
            if(data == null)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    data = connection.Query<T>(sql, param).ToList();
                    AddDataToCache<IEnumerable<T>>(data, cacheKey, expiration);
                }
            }

            return data;
        }

        public virtual bool Delete<T>(T obj, string cacheKey = "") where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    
                    RemoveFromCache(cacheKey);
                    return connection.Delete(obj);
                }
            }
            catch (Exception ex)
            {
                return false;
                //TODO: log this error
            }
        }

        public virtual int Execute(string sql, string cacheKey = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    
                    InvalidateCache(cacheKey);
                    return connection.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return -5;
            }
        }

        public virtual int ExecuteWithParam(string sql, object param, string cacheKey = "")
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    //InvalidateCache();
                    InvalidateCache(cacheKey);
                    return connection.Execute(sql, param);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return -1;
                //TODO: log this error
            }
        }

        public virtual int InsertList<T>(string sql, IEnumerable<T> dataList, string cacheKey = "", DateTime? expiration = null, bool deleteOldCache = false)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();

                    if (deleteOldCache)
                        RemoveFromCache(cacheKey);
                    AddDataToCache<IEnumerable<T>>(dataList, cacheKey, expiration);

                    return connection.Execute(sql, dataList);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return -1;
                //TODO: log this error
            }
        }

        

        public virtual bool Update<T>(T obj, string cacheKey = "", DateTime? expiration = null) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    
                    UpdateCache<T>(obj, cacheKey, expiration);
                    return connection.Update(obj);
                }
            }
            catch (Exception ex)
            {
                return false;
                //TODO: log this error
            }
            
        }

        protected void AddDataToCache<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if(CacheManager != null && !string.IsNullOrEmpty(cacheKey))
            {
                CacheManager.AddToCache<T>(data, cacheKey, expiration);
            }
        }

        protected T GetCachedItem<T>(string cacheKey)
        {
            if (CacheManager != null && !string.IsNullOrEmpty(cacheKey))
            {
                return CacheManager.GetCachedItem<T>(cacheKey);
            }
            return default(T);
        }

        protected IEnumerable<T> GetCachedList<T>(string cacheKey)
        {
            if (CacheManager != null)
            {
                return CacheManager.GetCachedList<T>(cacheKey);
            }
            return null;
        }

        protected void RemoveFromCache(string cacheKey)
        {
            if (CacheManager != null && !string.IsNullOrEmpty(cacheKey))
            {
                CacheManager.RemoveCache(cacheKey);
            }
        }

        protected void UpdateCache<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            if (CacheManager != null && !string.IsNullOrEmpty(cacheKey))
            {
                CacheManager.UpdateCache<T>(data, cacheKey, expiration);
            }
        }

        protected void InvalidateCache(string cacheKey)
        {
            if (CacheManager != null && !string.IsNullOrEmpty(cacheKey))
            {
                CacheManager.InvalideCache(cacheKey);
            }
        }

    }
}

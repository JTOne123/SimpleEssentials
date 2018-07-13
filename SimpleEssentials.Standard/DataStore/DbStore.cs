using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Reflection;
using FastMember;

namespace SimpleEssentials.DataStore
{
    public class DbStore : IDataStore
    {
        private string _connectionString { get; set; }

        public DbStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add<T>(T obj) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                long rowsAffected = connection.Insert(obj);
                if (rowsAffected == 0)
                    return false;
            }
            return true;
        }

        public int AddAndReturnId<T>(string sql, T obj) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<int>(sql, obj).FirstOrDefault();

            }
        }

        public int AddList<T>(IEnumerable<T> obj, string sql) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();


                return connection.Execute(sql, obj);
            }
        }

        public void BulkInsert<T>(IEnumerable<T> obj, string tableName) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    var properties = typeof(T).GetProperties();

                    using (var reader = ObjectReader.Create(obj, properties.Select(prop => prop.Name).ToArray()))
                    {
                        bulkCopy.BulkCopyTimeout = 120;
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(reader);
                    }
                }
            }

        }

        public bool Delete<T>(T obj) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Delete(obj);
            }
        }

        public T Get<T>(object id) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                return connection.Get<T>(id);
            }
        }

        public IEnumerable<T> GetByType<T>() where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.GetAll<T>();
            }
        }

        public IEnumerable<T> GetByParameters<T>(string sql, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<T>(sql, param);
            }
        }

        public IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<T, T2, T>(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<T, T2, T3, T>(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4>(string sql, Func<T, T2, T3, T4, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<T, T2, T3, T4, T>(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5>(string sql, Func<T, T2, T3, T4, T5, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<T, T2, T3, T4, T5, T>(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5, T6>(string sql, Func<T, T2, T3, T4, T5, T6, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Query<T, T2, T3, T4, T5, T6, T>(sql, func, param, splitOn: splitOn);

            }
        }

        public int Execute(string sql, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Execute(sql, param);
            }
        }

        public int ExecuteScalar(string sql, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.ExecuteScalar<int>(sql, param);
            }
        }

        public bool Update<T>(T obj) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection.Update(obj);
            }
        }


    }
}
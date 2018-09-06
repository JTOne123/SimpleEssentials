using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Dapper.Contrib.Extensions;
using SimpleEssentials.LinqToDb;
using SimpleEssentials.LinqToDb.Reflector;

namespace SimpleEssentials.ORM
{
    public class SqlProvider : IDbProvider
    {
        private string ConnectionString { get; set; }

        public SqlProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Insert<T>(T obj) where T : class, new()
        {
            if (obj is IEnumerable<T> list)
            {
                var sql = InsertSql(obj, typeof(T));
                return InsertList(list, sql) > 0;
            }

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                long rowsAffected = connection.Insert(obj);
                if (rowsAffected == 0)
                    return false;
            }
            return true;
        }

        internal string CreateTableSql<T>() where T : class, new()
        {
            var sqlReflector = new SqlReflector();
            var obj = new T();
            return $"CREATE TABLE {sqlReflector.GetTableName(obj)} {sqlReflector.GenerateCreateColumns(obj)}";
        }

        internal string InsertSql<T>(T obj, Type overrideType = null)
        {
            var sqlReflector = new SqlReflector();
            return $"insert into {sqlReflector.GetTableName(obj, overrideType)}{sqlReflector.GenerateInsertColumnNames(obj, overrideType)}";
        }

        public int InsertAndReturnId<T>(T obj) where T : class, new()
        {
            var sql = $"{InsertSql(obj)} select cast(scope_identity() as int)";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query<int>(sql, obj).FirstOrDefault();

            }
        }

        internal int InsertList<T>(IEnumerable<T> obj, string sql) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();


                return connection.Execute(sql, obj);
            }
        }

        /*
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
            
        }*/

        public bool Delete<T>(T obj) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Delete(obj);
            }
        }

        public IEnumerable<T> Get<T>() where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.GetAll<T>();
            }
        }

        public T Get<T>(object id) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.Get<T>(id);
            }
        }

        public IEnumerable<T> Get<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var whereClause = LinqToSql.Select<T>().Where(expression);
            return GetByParameters<T>(whereClause.Sql, whereClause.Parameters);
        }

        public IEnumerable<T> GetByParameters<T>(string sql, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query<T>(sql, param);
            }
        }

        public IEnumerable<T> GetMultiMap<T, T2>(string sql, Func<T, T2, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3>(string sql, Func<T, T2, T3, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4>(string sql, Func<T, T2, T3, T4, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5>(string sql, Func<T, T2, T3, T4, T5, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query(sql, func, param, splitOn: splitOn);

            }
        }

        public IEnumerable<T> GetMultiMap<T, T2, T3, T4, T5, T6>(string sql, Func<T, T2, T3, T4, T5, T6, T> func, object param = null, string splitOn = "")
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Query(sql, func, param, splitOn: splitOn);

            }
        }

        public int Execute(string sql, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Execute(sql, param);
            }
        }

        public int ExecuteScalar(string sql, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.ExecuteScalar<int>(sql, param);
            }
        }

        public bool Update<T>(T obj) where T : class, new()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                return connection.Update(obj);
            }
        }

        
    }
}
using System;
using System.Linq.Expressions;
using SimpleEssentials.Extensions;
using SimpleEssentials.Utils;

namespace SimpleEssentials.LinqToSQL
{
    public class Generator
    {
        public static string CreateTableSql<T>() where T : class, new()
        {
            var obj = new T();
            return $"CREATE TABLE {Reflection.GetTableName(obj)} {Reflection.GenerateCreateColumns(obj)}";
        }

        public static string InsertAndReturnIdSql<T>(T obj)
        {
            return $"{InsertSql(obj)} select cast(scope_identity() as int)";
        }

        public static string InsertSql<T>(T obj, Type overrideType = null)
        {
            return $"insert into {Reflection.GetTableName(obj, overrideType)}{Reflection.GenerateInsertColumnNames(obj, overrideType)}";
        }

        public static WherePart WhereSql<T>(Expression<Func<T, bool>> expression)
        {
            var type = typeof(T);
            var wherePart = ToSql(expression);
            wherePart.Sql = $"select * from {Reflection.GetTableName(type, type)} where {wherePart.Sql}";
            return wherePart;
        }

        public static WherePart ToSql<T>(Expression<Func<T, bool>> expression)
        {
            var i = 1;
            return LinqToSqlHelpers.Recurse(ref i, expression.Body, isUnary: true);
        }
        
    }
    
}

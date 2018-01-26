using System;
using System.Linq.Expressions;
using SimpleEssentials.Extensions;

namespace SimpleEssentials.LinqToSQL
{
    public class Generator
    {
        public static string InsertAndReturnIdSql<T>(T obj)
        {
            return $"{InsertSql(obj)} select cast(scope_identity() as int)";
        }

        public static string InsertSql<T>(T obj, Type overrideType = null)
        {
            return $"insert into {obj.GetTableName(overrideType)}{LinqToSqlHelpers.GenerateInsertColumnNames(obj, overrideType)}";
        }

        public static WherePart WhereSql<T>(Expression<Func<T, bool>> expression)
        {
            var type = typeof(T);
            var wherePart = ToSql(expression);
            wherePart.Sql = $"select * from {type.GetTableName(type)} where {wherePart.Sql}";
            return wherePart;
        }

        public static WherePart ToSql<T>(Expression<Func<T, bool>> expression)
        {
            var i = 1;
            return LinqToSqlHelpers.Recurse(ref i, expression.Body, isUnary: true);
        }
        
    }
    
}

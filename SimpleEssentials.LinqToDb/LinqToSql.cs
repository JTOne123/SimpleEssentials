using System;
using System.Linq.Expressions;
using SimpleEssentials.LinqToDb.Expression;
using SimpleEssentials.LinqToDb.Reflector;

namespace SimpleEssentials.LinqToDb
{
    public static class LinqToSql
    {
        private static readonly SqlReflector SqlReflector = new SqlReflector();

        public static IWherePart Where<T>(this CustomCommand command, Expression<Func<T, bool>> expression = null)
        {
            IWherePart where = null;

            if (expression != null)
            {
                where = ExpressionToSql.Convert(expression);
                command.Concat($" where {where.Sql}");
            }

            if (where == null)
                return null;

            where.Sql = command.GetCommand();
            where.Parameters = where.Parameters;
            return where;
        }

        public static CustomCommand Select<T>()
        {
            var command = new CustomCommand();
            var type = typeof(T);
            command.Concat($"select * from [{SqlReflector.GetTableName(type, type)}] [{SqlReflector.GetTableName(type, type)}]");
            return command;
        }

        public static CustomCommand InnerJoinOn<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            command.Concat($"inner join [{SqlReflector.GetTableName(type, type)}] [{SqlReflector.GetTableName(type, type)}]");
            command.Concat($"on {ExpressionToSql.Convert(expression).Sql}");
            return command;
        }

        public static CustomCommand LeftJoinOn<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            command.Concat($"left join [{SqlReflector.GetTableName(type, type)}] [{SqlReflector.GetTableName(type, type)}]");
            command.Concat($"on {ExpressionToSql.Convert(expression).Sql}");
            return command;
        }

        public static CustomCommand On<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            command.Concat($"on {ExpressionToSql.Convert(expression).Sql}");
            return command;
        }

        public static IWherePart Generate(this IWherePart wherePart)
        {
            return wherePart;
        }

        public static IWherePart Generate(this ICustomCommand command)
        {
            var where = new SqlWherePart
            {
                Sql = command.GetCommand()
            };
            return where;
        }

        
    }
}

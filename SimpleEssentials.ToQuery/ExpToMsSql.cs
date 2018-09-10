using System;
using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression;
using SimpleEssentials.ToQuery.Reflector;

namespace SimpleEssentials.ToQuery
{
    public static class ExpToMsSql
    {
        private static readonly SqlReflector SqlReflector = new SqlReflector();

        public static IQueryObject Where<T>(this CustomCommand command, Expression<Func<T, bool>> expression = null)
        {
            IQueryObject where = null;

            if (expression != null)
            {
                where = ExpressionToMsSql.Convert(expression);
                command.Concat($" where {where.Query}");
            }

            if (where == null)
                return null;

            where.Query = command.GetCommand();
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
            command.Concat($"on {ExpressionToMsSql.Convert(expression).Query}");
            return command;
        }

        public static CustomCommand LeftJoinOn<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            command.Concat($"left join [{SqlReflector.GetTableName(type, type)}] [{SqlReflector.GetTableName(type, type)}]");
            command.Concat($"on {ExpressionToMsSql.Convert(expression).Query}");
            return command;
        }

        public static CustomCommand On<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            command.Concat($"on {ExpressionToMsSql.Convert(expression).Query}");
            return command;
        }

        public static IQueryObject Generate(this IQueryObject wherePart)
        {
            return wherePart;
        }

        public static IQueryObject Generate(this ICustomCommand command)
        {
            var where = new SqlQueryObject
            {
                Query = command.GetCommand()
            };
            return where;
        }

        
    }
}

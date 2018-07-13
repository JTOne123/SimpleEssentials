using System;
using System.Linq.Expressions;
using SimpleEssentials.LinqToSQL;

namespace SimpleEssentials.Utils
{
    public static class SqlBuilder
    {
        public static WherePart Where<T>(this CustomCommand command, Expression<Func<T, bool>> expression = null)
        {
            var where = new WherePart();

            if (expression != null)
            {
                where = Generator.ToSql(expression);
                command.Concat($" where {where.Sql}");
            }
                
            where.Sql = command.GetCommand();
            where.Parameters = where.Parameters;
            return where;
        }

        public static CustomCommand Select<T>()
        {
            var command = new CustomCommand();
            var type = typeof(T);
            command.Concat($"select * from {Reflection.GetTableName(type, type)} {Reflection.GetTableName(type, type)}");
            return command;
        }

        public static CustomCommand Join<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            command.Concat($"inner join {Reflection.GetTableName(type, type)} {Reflection.GetTableName(type, type)}");
            command.Concat($"on {Generator.ToSql(expression).Sql}");
            return command;
        }

        public static CustomCommand JoinLeft<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            command.Concat($"left join {Reflection.GetTableName(type, type)} {Reflection.GetTableName(type, type)}");
            command.Concat($"on {Generator.ToSql(expression).Sql}");
            return command;
        }

        public static CustomCommand On<T, T2>(this CustomCommand command, Expression<Func<T, T2, bool>> expression)
        {
            command.Concat($"on {Generator.ToSql(expression).Sql}");
            return command;
        }

        public static WherePart Generate(this WherePart wherePart)
        {
            return wherePart;
        }

        public static WherePart Generate(this CustomCommand command)
        {
            var where = new WherePart
            {
                Sql = command.GetCommand()
            };
            return where;
        }
    }

    public class CustomCommand
    {
        private string Command { get; set; }

        public void Concat(string command)
        {
            Command += " " + command;
        }

        public string GetCommand()
        {
            return Command;
        }
    }
}

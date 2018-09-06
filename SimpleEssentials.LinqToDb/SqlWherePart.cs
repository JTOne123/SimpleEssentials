using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleEssentials.LinqToDb
{
    public class SqlWherePart : IWherePart
    {
        public string Sql { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public static IWherePart IsSql(string sql)
        {
            return new SqlWherePart()
            {
                Parameters = new Dictionary<string, object>(),
                Sql = sql
            };
        }

        public static IWherePart IsParameter(int count, object value)
        {
            return new SqlWherePart()
            {
                Parameters = { { count.ToString(), value } },
                Sql = $"@{count}"
            };
        }

        public static IWherePart IsCollection(ref int countStart, IEnumerable values)
        {
            var parameters = new Dictionary<string, object>();
            var sql = new StringBuilder("(");
            foreach (var value in values)
            {
                parameters.Add((countStart).ToString(), value);
                sql.Append($"@{countStart},");
                countStart++;
            }
            if (sql.Length == 1)
            {
                sql.Append("null,");
            }
            sql[sql.Length - 1] = ')';
            return new SqlWherePart()
            {
                Parameters = parameters,
                Sql = sql.ToString()
            };
        }

        public static IWherePart Concat(string @operator, IWherePart operand)
        {
            return new SqlWherePart()
            {
                Parameters = operand.Parameters,
                Sql = $"({@operator} {operand.Sql})"
            };
        }

        public static IWherePart Concat(IWherePart left, string @operator, IWherePart right)
        {
            return new SqlWherePart()
            {
                Parameters = left.Parameters.Union(right.Parameters).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Sql = $"({left.Sql} {@operator} {right.Sql})"
            };
        }
    }
}

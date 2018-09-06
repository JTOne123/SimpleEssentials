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

        public IWherePart IsSql(string sql)
        {
            return new SqlWherePart()
            {
                Parameters = new Dictionary<string, object>(),
                Sql = sql
            };
        }

        public void IsParameter(object value)
        {
            var count = Parameters.Count;
            this.Parameters.Add(count.ToString(), value);
            this.Sql += $"@{count}";
            /*return new SqlWherePart()
            {
                Parameters = { { count.ToString(), value } },
                Sql = $"@{count}"
            };*/
        }

        public IWherePart IsCollection(ref int countStart, IEnumerable values)
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

        public void Concat(string message)
        {
            this.Sql += $"{message}";
            /*return new SqlWherePart()
            {
                Parameters = wherePart.Parameters,
                Sql = $"{wherePart.Sql} {message}"
            };*/
        }

        /*public IWherePart Concat(string @operator, IWherePart operand)
        {
            return new SqlWherePart()
            {
                Parameters = operand.Parameters,
                Sql = $"({@operator} {operand.Sql})"
            };
        }*/

        public IWherePart Concat(IWherePart left, string @operator, IWherePart right)
        {
            return new SqlWherePart()
            {
                Parameters = left.Parameters.Union(right.Parameters).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Sql = $"({left.Sql} {@operator} {right.Sql})"
            };
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleEssentials.ToQuery
{
    public class QueryObject : IQueryObject
    {
        public string Query { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public IQueryObject IsQuery(string query)
        {
            return new QueryObject()
            {
                Parameters = new Dictionary<string, object>(),
                Query = query
            };
        }

        public void IsParameter(object value)
        {
            var count = Parameters.Count + 1;
            this.Parameters.Add(count.ToString(), value);
            this.Query += $"@{count}";
        }

        public IQueryObject IsCollection(ref int countStart, IEnumerable values)
        {
            var parameters = new Dictionary<string, object>();
            var query = new StringBuilder("(");
            foreach (var value in values)
            {
                parameters.Add((countStart).ToString(), value);
                query.Append($"@{countStart},");
                countStart++;
            }
            if (query.Length == 1)
            {
                query.Append("null,");
            }
            query[query.Length - 1] = ')';
            return new QueryObject()
            {
                Parameters = parameters,
                Query = query.ToString()
            };
        }

        public void Concat(string message)
        {
            this.Query += $"{message}";
        }

        public IQueryObject Concat(IQueryObject left, string @operator, IQueryObject right)
        {
            return new QueryObject()
            {
                Parameters = left.Parameters.Union(right.Parameters).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Query = $"({left.Query} {@operator} {right.Query})"
            };
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace SimpleEssentials.ToQuery
{
    public interface IQueryObject
    {
        string Query { get; set; }
        Dictionary<string, object> Parameters { get; set; }

        IQueryObject IsQuery(string query);

        void IsParameter(object value);

        IQueryObject IsCollection(ref int countStart, IEnumerable values);

        void Concat(string message);

        //IWherePart Concat(string @operator, IWherePart operand);

        IQueryObject Concat(IQueryObject left, string @operator, IQueryObject right);
    }
}

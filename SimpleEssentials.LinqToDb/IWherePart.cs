using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.LinqToDb
{
    public interface IWherePart
    {
        string Sql { get; set; }
        Dictionary<string, object> Parameters { get; set; }

        IWherePart IsSql(string sql);

        void IsParameter(object value);

        IWherePart IsCollection(ref int countStart, IEnumerable values);

        void Concat(string message);

        //IWherePart Concat(string @operator, IWherePart operand);

        IWherePart Concat(IWherePart left, string @operator, IWherePart right);
    }
}

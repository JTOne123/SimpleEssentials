using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.LinqToDb
{
    public interface IWherePart
    {
        string Sql { get; set; }
        Dictionary<string, object> Parameters { get; set; }
    }
}

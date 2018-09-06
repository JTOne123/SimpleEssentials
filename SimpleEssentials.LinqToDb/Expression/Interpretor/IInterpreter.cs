using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SimpleEssentials.LinqToDb.Expression.Interpretor
{
    public interface IInterpreter
    {
        string NodeTypeToString(ExpressionType nodeType);
        string WildcardCharacter { get; set; }
        IWherePart WherePart { get; set; }
    }
}

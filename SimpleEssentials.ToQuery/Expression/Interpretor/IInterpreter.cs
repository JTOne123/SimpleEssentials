using System.Linq.Expressions;

namespace SimpleEssentials.ToQuery.Expression.Interpretor
{
    public interface IInterpreter
    {
        string NodeTypeToString(ExpressionType nodeType);
        string WildcardCharacter { get; set; }
        IQueryObject WherePart { get; set; }
    }
}

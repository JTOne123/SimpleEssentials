using System.Linq.Expressions;

namespace SimpleEssentials.ToQuery.Expression.Interpretor
{
    public interface IInterpreter
    {
        string NodeTypeToString(ExpressionType nodeType);
        string WildcardCharacter { get; set; }
        char[] DelimitedCharacters {get;}
        IQueryObject WherePart { get; set; }
    }
}

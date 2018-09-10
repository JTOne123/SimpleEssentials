using System;
using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;
using SimpleEssentials.ToQuery.Expression.Visitors;

namespace SimpleEssentials.ToQuery.Expression
{
    internal class ExpressionToQuery
    {
        public static IQueryObject Convert<T>(Expression<Func<T, bool>> expression, IInterpreter interpreter)
        {
            Visitor.CreateFromExpression(expression, ref interpreter).Visit();
            return interpreter.WherePart;
        }

        public static IQueryObject Convert<T, T2>(Expression<Func<T, T2, bool>> expression, IInterpreter interpreter)
        {
            Visitor.CreateFromExpression(expression, ref interpreter).Visit();
            return interpreter.WherePart;
        }
    }
}

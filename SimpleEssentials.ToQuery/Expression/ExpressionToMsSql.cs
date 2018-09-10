using System;
using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;
using SimpleEssentials.ToQuery.Expression.Visitors;

namespace SimpleEssentials.ToQuery.Expression
{
    internal class ExpressionToMsSql
    {
        public static IQueryObject Convert<T>(Expression<Func<T, bool>> expression)
        {
            IInterpreter interpretor = new MsSqlInterpreter();
            Visitor.CreateFromExpression(expression, ref interpretor).Visit();
            return interpretor.WherePart;
        }

        public static IQueryObject Convert<T, T2>(Expression<Func<T, T2, bool>> expression)
        {
            IInterpreter interpretor = new MsSqlInterpreter();
            Visitor.CreateFromExpression(expression, ref interpretor).Visit();
            return interpretor.WherePart;
        }
    }
}

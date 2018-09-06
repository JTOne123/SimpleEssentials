using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleEssentials.LinqToDb.Expression.Interpretor;
using SimpleEssentials.LinqToDb.Expression.Visitors;
using SimpleEssentials.LinqToDb.Reflector;

namespace SimpleEssentials.LinqToDb.Expression
{
    public class ExpressionToSql
    {
        

        public static IWherePart Convert<T>(Expression<Func<T, bool>> expression)
        {
            IInterpreter interpretor = new SqlInterpreter();
            Visitor.CreateFromExpression(expression, ref interpretor).Visit();
            return interpretor.WherePart;
        }

        public static IWherePart Convert<T, T2>(Expression<Func<T, T2, bool>> expression)
        {
            IInterpreter interpretor = new SqlInterpreter();
            Visitor.CreateFromExpression(expression, ref interpretor).Visit();
            return interpretor.WherePart;
        }
    }
}

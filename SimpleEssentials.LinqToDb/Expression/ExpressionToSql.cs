using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleEssentials.LinqToDb.Reflector;

namespace SimpleEssentials.LinqToDb.Expression
{
    internal static class ExpressionToSql
    {
        private static readonly IReflector SqlReflector = new SqlReflector();

        public static IWherePart Recurse(ref int i, System.Linq.Expressions.Expression expression, bool isUnary = false, string prefix = null, string postfix = null)
        {
            if (expression is LambdaExpression lambda)
            {
                Console.WriteLine("Hello!");
            }
            if (expression is BlockExpression block)
            {
                Console.WriteLine("Hello!");
            }
            if (expression is ConditionalExpression conditional)
            {
                Console.WriteLine("Hello!");
            }
            if (expression is DynamicExpression dynamicExp)
            {
                Console.WriteLine("Hello!");
            }
            if (expression is InvocationExpression invoExp)
            {
                Console.WriteLine("Hello!");
            }
            if (expression is NewExpression newExp)
            {
                Console.WriteLine("Hello!");
            }
            if (expression is RuntimeVariablesExpression runetimeExp)
            {
                Console.WriteLine("Hello!");
            }
            //source: http://ryanohs.com/2016/04/generating-sql-from-expression-trees-part-2/
            if (expression is UnaryExpression unary)
            {
                return SqlWherePart.Concat(NodeTypeToString(unary.NodeType), Recurse(ref i, unary.Operand, true));
            }
            if (expression is BinaryExpression body)
            {
                return SqlWherePart.Concat(Recurse(ref i, body.Left), NodeTypeToString(body.NodeType), Recurse(ref i, body.Right));
            }
            if (expression is ConstantExpression constant)
            {
                var value = constant.Value;
                if (value is int)
                {
                    return SqlWherePart.IsSql(value.ToString());
                }
                if (value is string)
                {
                    value = prefix + (string)value + postfix;
                }
                if (value is bool && isUnary)
                {
                    return SqlWherePart.Concat(SqlWherePart.IsParameter(i++, value), "=", SqlWherePart.IsSql("1"));
                }
                return SqlWherePart.IsParameter(i++, value);
            }
            if (expression is MemberExpression member)
            {
                if (member.Member is PropertyInfo property)
                {
                    var colName = property.Name;
                    if (isUnary && member.Type == typeof(bool))
                    {
                        return SqlWherePart.Concat(Recurse(ref i, expression), "=", SqlWherePart.IsParameter(i++, true));
                    }

                    if (colName == "Now")
                    {
                        var reduced = expression.Reduce();
                        var val = System.Linq.Expressions.Expression.Lambda(expression).Compile().DynamicInvoke();
                        return SqlWherePart.IsParameter(i++, val);
                    }
                    return SqlWherePart.IsSql("[" + colName + "]");
                }
                if (member.Member is FieldInfo)
                {
                    var value = GetValue(member);
                    if (value is string)
                    {
                        value = prefix + (string)value + postfix;
                    }
                    return SqlWherePart.IsParameter(i++, value);
                }
                throw new Exception($"Expression does not refer to a property or field: {expression}");
            }
            if (expression is MethodCallExpression methodCall)
            {
                // LIKE queries:
                if (methodCall.Method == typeof(string).GetMethod("Contains", new[] { typeof(string) }))
                {
                    return SqlWherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "%", postfix: "%"));
                }
                if (methodCall.Method == typeof(string).GetMethod("StartsWith", new[] { typeof(string) }))
                {
                    return SqlWherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], postfix: "%"));
                }
                if (methodCall.Method == typeof(string).GetMethod("EndsWith", new[] { typeof(string) }))
                {
                    return SqlWherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "%"));
                }
                // IN queries:
                if (methodCall.Method.Name == "Contains")
                {
                    System.Linq.Expressions.Expression collection;
                    System.Linq.Expressions.Expression property;
                    if (methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 2)
                    {
                        collection = methodCall.Arguments[0];
                        property = methodCall.Arguments[1];
                    }
                    else if (!methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 1)
                    {
                        collection = methodCall.Object;
                        property = methodCall.Arguments[0];
                    }
                    else
                    {
                        throw new Exception("Unsupported method call: " + methodCall.Method.Name);
                    }
                    var values = (IEnumerable)GetValue(collection);
                    return SqlWherePart.Concat(Recurse(ref i, property), "IN", SqlWherePart.IsCollection(ref i, values));
                }

                //Everything else - like datetime.now
                var val = System.Linq.Expressions.Expression.Lambda(methodCall).Compile().DynamicInvoke();
                return SqlWherePart.IsParameter(i++, val);
            }
            throw new Exception("Unsupported expression: " + expression.GetType().Name);
        }

        public static object GetValue(System.Linq.Expressions.Expression member)
        {
            // source: http://stackoverflow.com/a/2616980/291955
            var objectMember = System.Linq.Expressions.Expression.Convert(member, typeof(object));
            var getterLambda = System.Linq.Expressions.Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }

        public static string NodeTypeToString(ExpressionType nodeType)
        {
            //source: http://ryanohs.com/2016/04/generating-sql-from-expression-trees-part-2/
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Negate:
                    return "-";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Convert:
                    return "";
            }
            throw new Exception($"Unsupported node type: {nodeType}");
        }

        public static IWherePart Run<T>(Expression<Func<T, bool>> expression)
        {
            var i = 1;
            return Recurse(ref i, expression.Body, isUnary: true);
        }

        public static IWherePart Run<T, T2>(Expression<Func<T, T2, bool>> expression)
        {
            var i = 1;
            return Recurse(ref i, expression.Body, isUnary: true);
        }
    }
}

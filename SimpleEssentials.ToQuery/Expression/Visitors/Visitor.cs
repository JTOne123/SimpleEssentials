using System;
using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
    public abstract class Visitor
    {
        private readonly System.Linq.Expressions.Expression node;
        public IInterpreter Interpretor;

        protected Visitor(System.Linq.Expressions.Expression node)
        {
            this.node = node;
        }

        public abstract void Visit(string prefix = "", string postfix = "");

        public ExpressionType NodeType => this.node.NodeType;
        public static Visitor CreateFromExpression(System.Linq.Expressions.Expression node, ref IInterpreter interpreter)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Constant:
                    return new ConstantVisitor((ConstantExpression)node, ref interpreter);
                case ExpressionType.Lambda:
                    return new LambdaVisitor((LambdaExpression)node, ref interpreter);
                case ExpressionType.Parameter:
                    return new ParameterVisitor((ParameterExpression)node, ref interpreter);
                case ExpressionType.Add:
                case ExpressionType.Equal:
                case ExpressionType.Multiply:
                case ExpressionType.AndAlso:
                case ExpressionType.NotEqual:
                case ExpressionType.OrElse:
                    return new BinaryVisitor((BinaryExpression)node, ref interpreter);
                case ExpressionType.Conditional:
                    return new ConditionalVisitor((ConditionalExpression)node, ref interpreter);
                case ExpressionType.Call:
                    return new MethodCallVisitor((MethodCallExpression)node, ref interpreter);
                case ExpressionType.MemberAccess:
                    return new MemberVisitor((MemberExpression)node, ref interpreter);
                case ExpressionType.Convert:
                    return new ConvertVisitor((System.Linq.Expressions.Expression)node, ref interpreter);
                default:
                    Console.Error.WriteLine($"Node not processed yet: {node.NodeType}");
                    return default(Visitor);
            }
        }
    }
}

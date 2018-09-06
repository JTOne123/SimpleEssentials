using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SimpleEssentials.LinqToDb.Expression.Interpretor;

namespace SimpleEssentials.LinqToDb.Expression.Visitors
{
    public class LambdaVisitor : Visitor
    {
        private readonly LambdaExpression node;
        public LambdaVisitor(LambdaExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            //Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
            //Console.WriteLine($"{prefix}The name of the lambda is {((node.Name == null) ? "<null>" : node.Name)}");
            //Console.WriteLine($"{prefix}The return type is {node.ReturnType.ToString()}");
            //Console.WriteLine($"{prefix}The expression has {node.Parameters.Count} argument(s). They are:");
            // Visit each parameter:
            foreach (var argumentExpression in node.Parameters)
            {
                var argumentVisitor = Visitor.CreateFromExpression(argumentExpression, ref this.Interpretor);
                argumentVisitor.Visit(prefix, postfix);
            }
            //Console.WriteLine($"{prefix}The expression body is:");
            // Visit the body:
            var bodyVisitor = Visitor.CreateFromExpression(node.Body, ref this.Interpretor);
            bodyVisitor.Visit();
        }
    }
}

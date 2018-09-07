using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
    public class ConditionalVisitor : Visitor
    {
        private readonly ConditionalExpression node;
        public ConditionalVisitor(ConditionalExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            //Console.WriteLine($"{prefix}This expression is a {NodeType} expression");
            var testVisitor = Visitor.CreateFromExpression(node.Test, ref this.Interpretor);
           // Console.WriteLine($"{prefix}The Test for this expression is:");
            testVisitor.Visit(prefix, postfix);
            var trueVisitor = Visitor.CreateFromExpression(node.IfTrue, ref this.Interpretor);
            //Console.WriteLine($"{prefix}The True clause for this expression is:");
            trueVisitor.Visit(prefix, postfix);
            var falseVisitor = Visitor.CreateFromExpression(node.IfFalse, ref this.Interpretor);
            //Console.WriteLine($"{prefix}The False clause for this expression is:");
            falseVisitor.Visit(prefix, postfix);
        }
    }
}

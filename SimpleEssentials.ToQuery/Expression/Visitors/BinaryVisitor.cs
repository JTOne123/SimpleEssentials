using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
    public class BinaryVisitor : Visitor
    {
        private readonly BinaryExpression node;
        public BinaryVisitor(BinaryExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            //Console.WriteLine($"{prefix}This binary expression is a {NodeType} expression");
            Visitor.CreateFromExpression(node.Left, ref this.Interpretor).Visit(prefix, postfix);
            this.Interpretor.WherePart.Concat($" {this.Interpretor.NodeTypeToString(NodeType)} ");
            Visitor.CreateFromExpression(node.Right, ref this.Interpretor).Visit(prefix, postfix);
        }
    }
}

using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
    public class ParameterVisitor : Visitor
    {
        private readonly ParameterExpression node;
        public ParameterVisitor(ParameterExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            //Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            //Console.WriteLine($"{prefix}Type: {node.Type.ToString()}, Name: {node.Name}, ByRef: {node.IsByRef}");
        }
    }
}

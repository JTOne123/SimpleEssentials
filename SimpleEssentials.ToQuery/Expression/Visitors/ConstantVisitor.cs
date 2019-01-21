using System;
using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
    public class ConstantVisitor : Visitor
    {
        private readonly ConstantExpression node;
        public ConstantVisitor(ConstantExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            //Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            //Console.WriteLine($"{prefix}The type of the constant value is {node.Type}");
            //Console.WriteLine($"{prefix}The value of the constant value is {node.Value}");

            if (node.Type == typeof(string))
                this.Interpretor.WherePart.Concat($"'{prefix}{node.Value.ToString()}{postfix}'");
            else if (node.Type == typeof(bool))
                this.Interpretor.WherePart.Concat($"{prefix}{Convert.ToInt32(node.Value)}{postfix}");
            else
                this.Interpretor.WherePart.Concat($"{prefix}{node.Value.ToString()}{postfix}");

        }
    }
}

using System;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
    public class ConvertVisitor : Visitor
    {
        private readonly System.Linq.Expressions.Expression node;
        public ConvertVisitor(System.Linq.Expressions.Expression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            var val = GetValue(node);
            //Console.WriteLine("Testing");
            //this.Interpretor.WherePart = this.Interpretor.WherePart.Concat($"'{val}'", this.Interpretor.WherePart);
            this.Interpretor.WherePart.IsParameter(val);
        }

        public object GetValue(System.Linq.Expressions.Expression member)
        {
            // source: http://stackoverflow.com/a/2616980/291955
            var objectMember = System.Linq.Expressions.Expression.Convert(member, typeof(object));
            var getterLambda = System.Linq.Expressions.Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Reflection;
using SimpleEssentials.ToQuery.Expression.Interpretor;

namespace SimpleEssentials.ToQuery.Expression.Visitors
{
   public class MemberVisitor : Visitor
    {
        private readonly MemberExpression node;
        public MemberVisitor(MemberExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            if (node.Member is PropertyInfo property)
            {
                var propName = property.Name;
                //Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
                //Console.WriteLine($"{prefix}The name of the property is {propName}");
                //Console.WriteLine($"{prefix}The return type is {node.Type}");

                //Interpretor.WherePart.Sql += $"[{propName}]";
                if (node.Member.DeclaringType != null)
                    this.Interpretor.WherePart.Concat(
                        $"{this.Interpretor.DelimitedCharacters[0]}{node.Member.DeclaringType.Name}{this.Interpretor.DelimitedCharacters[1]}.{this.Interpretor.DelimitedCharacters[0]}{prefix}{propName}{postfix}{this.Interpretor.DelimitedCharacters[1]}");
                else
                    this.Interpretor.WherePart.Concat($"{this.Interpretor.DelimitedCharacters[0]}{prefix}{propName}{postfix}{this.Interpretor.DelimitedCharacters[1]}");

            }
            if (node.Member is FieldInfo fieldInfo)
            {
                var fieldName = fieldInfo.Name;
                //Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
                //Console.WriteLine($"{prefix}The name of the FieldInfo is {fieldName}");
                //Console.WriteLine($"{prefix}The return type is {node.Type}");
                //Interpretor.WherePart.Sql += $"[{fieldName}]";
                //this.Interpretor.WherePart.Concat($"[{prefix}{fieldName}{postfix}]");
                var value = GetValue(node);
                this.Interpretor.WherePart.IsParameter(value);

            }
            //throw new Exception($"Expression does not refer to a property or field: {node}");
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

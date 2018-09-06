using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SimpleEssentials.LinqToDb.Expression.Interpretor;

namespace SimpleEssentials.LinqToDb.Expression.Visitors
{
    public class MethodCallVisitor : Visitor
    {
        private readonly MethodCallExpression node;
        public MethodCallVisitor(MethodCallExpression node, ref IInterpreter interpreter) : base(node)
        {
            this.node = node;
            this.Interpretor = interpreter;
        }

        public override void Visit(string prefix = "", string postfix = "")
        {
            //Console.WriteLine($"{prefix}This expression is a {NodeType} expression");

            /*if (node.Object == null)
                Console.WriteLine($"{prefix}This is a static method call");
            else
            {
                Console.WriteLine($"{prefix}The receiver (this) is:");
                var receiverVisitor = Visitor.CreateFromExpression(node.Object, ref this.Interpretor);
                receiverVisitor.Visit(prefix + "\t", methodToGetData);
            }*/

            //var methodInfo = node.Method;
            //Console.WriteLine($"{prefix}The method name is {methodInfo.DeclaringType}.{methodInfo.Name}");
            // There is more here, like generic arguments, and so on.
            //Console.WriteLine($"{prefix}The Arguments are:");

            /*foreach (var arg in node.Arguments)
            {
                var argVisitor = Visitor.CreateFromExpression(arg, ref this.Interpretor);
                argVisitor.Visit(prefix + "\t", methodToGetData);
            }*/

            if (node is MethodCallExpression methodCall)
            {
                //string EQUAL query:
                if (methodCall.Method == typeof(string).GetMethod("Equals", new[] { typeof(string) }))
                {
                    Visitor.CreateFromExpression(node.Object, ref this.Interpretor).Visit();
                    this.Interpretor.WherePart.Concat(" = ");
                    Visitor.CreateFromExpression(node.Arguments[0], ref this.Interpretor).Visit(this.Interpretor.WildcardCharacter, this.Interpretor.WildcardCharacter);
                    return;
                }

                //other EQUAL queries:
                if (methodCall.Method == typeof(object).GetMethod("Equals", new[] { typeof(string) }))
                {
                    Visitor.CreateFromExpression(node.Object, ref this.Interpretor).Visit();
                    this.Interpretor.WherePart.Concat(" = ");
                    Visitor.CreateFromExpression(node.Arguments[0], ref this.Interpretor).Visit();
                    return;
                }

                // LIKE queries:
                if (methodCall.Method == typeof(string).GetMethod("Contains", new[] { typeof(string) }))
                {
                    Visitor.CreateFromExpression(node.Object, ref this.Interpretor).Visit();
                    this.Interpretor.WherePart.Concat(" LIKE ");
                    Visitor.CreateFromExpression(node.Arguments[0], ref this.Interpretor).Visit(this.Interpretor.WildcardCharacter, this.Interpretor.WildcardCharacter);
                    return;
                }
                if (methodCall.Method == typeof(string).GetMethod("StartsWith", new[] { typeof(string) }))
                {
                    Visitor.CreateFromExpression(node.Object, ref this.Interpretor).Visit();
                    this.Interpretor.WherePart.Concat(" LIKE ");
                    Visitor.CreateFromExpression(node.Arguments[0], ref this.Interpretor).Visit("", this.Interpretor.WildcardCharacter);
                    return;
                }
                if (methodCall.Method == typeof(string).GetMethod("EndsWith", new[] { typeof(string) }))
                {
                    Visitor.CreateFromExpression(node.Object, ref this.Interpretor).Visit();
                    this.Interpretor.WherePart.Concat(" LIKE ");
                    Visitor.CreateFromExpression(node.Arguments[0], ref this.Interpretor).Visit(this.Interpretor.WildcardCharacter, "");
                    return;
                }
                // IN queries:
                if (methodCall.Method.Name == "Contains")
                {
                    this.Interpretor.WherePart.Concat("");
                    /*System.Linq.Expressions.Expression collection;
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
                    return SqlWherePart.Concat(Recurse(ref i, property), "IN", SqlWherePart.IsCollection(ref i, values));*/
                }

                //Everything else - like datetime.now
                //var val = System.Linq.Expressions.Expression.Lambda(methodCall).Compile().DynamicInvoke();
                //return SqlWherePart.IsParameter(i++, val);
            }
        }
    }
}

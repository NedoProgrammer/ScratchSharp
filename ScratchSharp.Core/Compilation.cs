using System.Text.RegularExpressions;
using ScratchSharp.Core.Binding;
using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Core;

public class Compilation
{
    public SyntaxTree Tree { get; }

    public Compilation(SyntaxTree tree)
    {
        Tree = tree;
    }

    public object Evaluate(Dictionary<string, object> variables)
    {
        var binder = new Binder(variables);
        var boundExpression = binder.Bind(Tree.Root);
        var evaluator = new Evaluator(boundExpression, variables);
        var value = evaluator.Evaluate();
        return value;
    }
}
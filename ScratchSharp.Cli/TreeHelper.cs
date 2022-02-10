using ScratchSharp.Core.Syntax;
using Spectre.Console;

namespace ScratchSharp.Cli;

public class TreeHelper
{
    private static Tree _tree;
    
    public static void PrintTree(SyntaxTree tree)
    {
        _tree = new Tree("")
            .Guide(TreeGuide.Line);
        AddNode(null, tree.Root);
        AnsiConsole.Write(_tree);
    }

    private static void AddNode(TreeNode? treeNode, SyntaxNode? node)
    {
        if (node == null) return;
        if (treeNode == null)
        {
            var root = _tree.AddNode(node.Kind.ToString());
            foreach(var child in node.GetChildren())
                AddNode(root, child);
        }
        else
        {
            var toAdd = node.Kind.ToString();
            if (node is SyntaxToken {Value: { }} st) toAdd += $" ({st.Value})";
            var child = treeNode.AddNode(toAdd);
            foreach(var syntaxChildren in node.GetChildren())
                AddNode(child, syntaxChildren);
        }
    }
}
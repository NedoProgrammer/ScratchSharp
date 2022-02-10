namespace ScratchSharp.Core.Syntax;

public class TreeViewer
{
    public static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
    {
        var marker = isLast ? "└──": "├──";
        Console.Write(indent);
        Console.Write(marker);
        Console.Write(node.Kind);
        if (node is SyntaxToken {Value: { }} t)
        {
            Console.Write(" ");
            Console.Write(t.Value);
        }
        Console.WriteLine();

        indent += isLast ? "   " : "│   ";
        var lastNode = node.GetChildren().LastOrDefault();
        
        foreach(var child in node.GetChildren())
            PrettyPrint(child, indent, child == lastNode);
    }
}
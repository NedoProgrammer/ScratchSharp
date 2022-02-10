namespace ScratchSharp.Core.Syntax;

public class SyntaxTree
{
    public Expression Root { get; }
    public SyntaxToken EndOfFile { get; }

    public SyntaxTree(Expression root, SyntaxToken endOfFile)
    {
        Root = root;
        EndOfFile = endOfFile;
    }

    public static SyntaxTree Parse(string text)
    {
        var parser = new Parser(text);
        return parser.Parse();
    }
}
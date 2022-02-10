namespace ScratchSharp.Core.Syntax;

public class NameExpressionSyntax: Expression
{
    public SyntaxToken Identifier { get; }

    public NameExpressionSyntax(SyntaxToken identifier)
    {
        Identifier = identifier;
    }

    public override SyntaxKind Kind => SyntaxKind.NameExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Identifier;
    }
}
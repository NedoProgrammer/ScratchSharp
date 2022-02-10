namespace ScratchSharp.Core.Syntax;

public class LiteralExpressionSyntax: Expression
{
    public object Value { get; }
    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }

    public SyntaxToken LiteralToken;
    public LiteralExpressionSyntax(SyntaxToken literal, object value)
    {
        Value = value;
        LiteralToken = literal;
    }
    
    public LiteralExpressionSyntax(SyntaxToken token):this(token, token.Value) {}
}
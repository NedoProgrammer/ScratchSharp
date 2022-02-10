namespace ScratchSharp.Core.Syntax;

public class ParanthesizedExpressionSyntax: Expression
{
    public SyntaxToken Open { get; }
    public Expression Expression { get; }
    public SyntaxToken Close { get; }

    public ParanthesizedExpressionSyntax(SyntaxToken open, Expression expression, SyntaxToken close)
    {
        Open = open;
        Expression = expression;
        Close = close;
    }

    public override SyntaxKind Kind => SyntaxKind.ParanthesizedExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Open;
        yield return Expression;
        yield return Close;
    }
}
namespace ScratchSharp.Core.Syntax;

public class BinaryExpressionSyntax: Expression
{
    public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Left;
        yield return Operator;
        yield return Right;
    }

    public Expression Left { get; }
    public SyntaxNode Operator { get; }
    public Expression Right { get; }
    
    public BinaryExpressionSyntax(Expression left, SyntaxToken op, Expression right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}
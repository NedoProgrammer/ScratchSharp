namespace ScratchSharp.Core.Syntax;

public class UnaryExpressionSyntax: Expression
{
    public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Operator;
        yield return Operand;
    }
    
    public SyntaxNode Operator { get; }
    public Expression Operand { get; }
    
    public UnaryExpressionSyntax(SyntaxToken op, Expression operand)
    {
        Operator = op;
        Operand = operand;
    }
}
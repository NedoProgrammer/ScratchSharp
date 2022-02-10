namespace ScratchSharp.Core.Syntax;

public class AssignmentExpressionSyntax: Expression
{
    public SyntaxToken Identifier { get; }
    public SyntaxToken Equals { get; }
    public Expression Expression { get; }

    public AssignmentExpressionSyntax(SyntaxToken identifier, SyntaxToken equals, Expression expression)
    {
        Identifier = identifier;
        Equals = equals;
        Expression = expression;
    }

    public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Identifier;
        yield return Equals;
        yield return Expression;
    }
}
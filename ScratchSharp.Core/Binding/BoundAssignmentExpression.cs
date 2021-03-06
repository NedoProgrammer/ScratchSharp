namespace ScratchSharp.Core.Binding;

public class BoundAssignmentExpression: BoundExpression
{
    public string Name { get; }
    public BoundExpression Expression { get; }

    public BoundAssignmentExpression(string name, BoundExpression expression)
    {
        Name = name;
        Expression = expression;
    }
    public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    public override Type Type => Expression.Type;
}
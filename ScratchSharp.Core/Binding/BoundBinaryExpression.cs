namespace ScratchSharp.Core.Binding;

internal sealed class BoundBinaryExpression : BoundExpression
{
    
    
    public BoundExpression Left { get; }
    public BoundExpression Right { get; }
    public BoundBinaryOperator Operator { get; }

    public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
    {
        Left = left;
        Right = right;
        Operator = op;
    }

    public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
    public override Type Type => Operator.ResultType;
}
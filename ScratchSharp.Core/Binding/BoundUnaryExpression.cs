namespace ScratchSharp.Core.Binding;

internal sealed class BoundUnaryExpression : BoundExpression
{
    public BoundUnaryOperator Operator { get; }
    public BoundExpression Operand { get; }

    public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
    {
        Operator = op;
        Operand = operand;
    }


    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    public override Type Type => Operator.ResultType;
}
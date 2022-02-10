namespace ScratchSharp.Core.Binding;

public abstract class BoundExpression : BoundNode
{
    public abstract Type Type { get; }
}
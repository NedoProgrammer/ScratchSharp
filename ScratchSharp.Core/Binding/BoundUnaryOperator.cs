using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Core.Binding;
internal sealed class BoundUnaryOperator
{
    public SyntaxKind Kind { get; }
    public BoundUnaryOperatorKind OperatorKind { get; }
    public Type OperandType { get; }
    public Type ResultType { get; }


    private BoundUnaryOperator(SyntaxKind kind, BoundUnaryOperatorKind operatorKind, Type operandType):
        this(kind, operatorKind, operandType, operandType) {}
        
    private BoundUnaryOperator(SyntaxKind kind, BoundUnaryOperatorKind operatorKind, Type operandType, Type resultType)
    {
        Kind = kind;
        OperatorKind = operatorKind;
        OperandType = operandType;
        ResultType = resultType;
    }

    private static readonly BoundUnaryOperator[] _operators =
    {
        new(SyntaxKind.Not, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),
        new(SyntaxKind.Plus, BoundUnaryOperatorKind.Identity, typeof(int)),
        new(SyntaxKind.Minus, BoundUnaryOperatorKind.Negation, typeof(int))
    };

    public static BoundUnaryOperator? Bind(SyntaxKind kind, Type operandType) => _operators.FirstOrDefault(x => x.Kind == kind && x.OperandType == operandType);
}
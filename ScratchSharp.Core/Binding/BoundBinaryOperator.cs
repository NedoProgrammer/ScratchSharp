using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Core.Binding;
internal sealed class BoundBinaryOperator
{
    public SyntaxKind Kind { get; }
    public BoundBinaryOperatorKind OperatorKind { get; }
    public Type LeftType { get; }
    public Type RightType { get; }
    public Type ResultType { get; }
    
    private BoundBinaryOperator(SyntaxKind kind, BoundBinaryOperatorKind operatorKind, Type leftType, Type rightType, Type resultType)
    {
        Kind = kind;
        OperatorKind = operatorKind;
        LeftType = leftType;
        RightType = rightType;
        ResultType = resultType;
    }
    
    private BoundBinaryOperator(SyntaxKind kind, BoundBinaryOperatorKind operatorKind, Type type):
        this(kind, operatorKind, type, type, type) {}
    
    private BoundBinaryOperator(SyntaxKind kind, BoundBinaryOperatorKind operatorKind, Type operandType, Type resultType):
        this(kind, operatorKind, operandType, operandType, resultType) {}

    private static readonly BoundBinaryOperator[] _operators =
    {
        new(SyntaxKind.Plus, BoundBinaryOperatorKind.Addition, typeof(int)),
        new(SyntaxKind.Minus, BoundBinaryOperatorKind.Subtraction, typeof(int)),
        new(SyntaxKind.Multiply, BoundBinaryOperatorKind.Multiplication, typeof(int)),
        new(SyntaxKind.Divide, BoundBinaryOperatorKind.Division, typeof(int)),
        new(SyntaxKind.And, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
        new(SyntaxKind.Or, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
        new(SyntaxKind.Equals, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
        new(SyntaxKind.NotEquals, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),
        new(SyntaxKind.Equals, BoundBinaryOperatorKind.Equals, typeof(bool)),
        new(SyntaxKind.NotEquals, BoundBinaryOperatorKind.NotEquals, typeof(bool))
    };

    public static BoundBinaryOperator? Bind(SyntaxKind kind, Type leftType, Type rightType) => _operators.FirstOrDefault(x => x.Kind == kind && x.LeftType == leftType && x.RightType == rightType);
}
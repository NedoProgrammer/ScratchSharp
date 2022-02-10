using ScratchSharp.Core.Binding;

namespace ScratchSharp.Core;

public class Evaluator
{
    private readonly Dictionary<string, object> _dictionary;
    public BoundExpression Root { get; }

    public Evaluator(BoundExpression root, Dictionary<string, object> dictionary)
    {
        _dictionary = dictionary;
        Root = root;
    }

    public object Evaluate()
    {
        return EvaluateExpression(Root);
    }

    public object EvaluateExpression(BoundExpression root)
    {
        if (root is BoundLiteralExpression n)
            return n.Value;
        if (root is BoundVariableExpression v)
            return _dictionary[v.Name];
        if (root is BoundAssignmentExpression a)
        {
            var value = EvaluateExpression(a.Expression);
            _dictionary[a.Name] = value;
            return value;
        }

        if (root is BoundBinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);
            return b.Operator.OperatorKind switch
            {
                BoundBinaryOperatorKind.Addition => (int)left + (int)right,
                BoundBinaryOperatorKind.Subtraction => (int)left - (int)right,
                BoundBinaryOperatorKind.Multiplication => (int)left * (int)right,
                BoundBinaryOperatorKind.Division => (int)left / (int)right,
                BoundBinaryOperatorKind.LogicalAnd => (bool)left && (bool)right,
                BoundBinaryOperatorKind.LogicalOr => (bool)left || (bool)right,
                BoundBinaryOperatorKind.Equals => Equals(left, right),
                BoundBinaryOperatorKind.NotEquals => !Equals(left, right),
                _ => throw new ArgumentOutOfRangeException()
            };
            //TODO: HANDLE UNKNOWN BINARY OPERATOR
        }

        if (root is BoundUnaryExpression u)
        {
            var operand = EvaluateExpression(u.Operand);
            switch (u.Operator.OperatorKind)
            {
                case BoundUnaryOperatorKind.Negation:
                    return -(int)operand;
                case BoundUnaryOperatorKind.Identity:
                    return (int)operand;
                case BoundUnaryOperatorKind.LogicalNegation:
                    return !(bool) operand;
            }

            //TODO: HANDLE UNEXPECTED UNARY OPERATOR
        }

        /*if (root is ParanthesizedExpressionSyntax p)
            return EvaluateExpression(p.Expression);*/
        //TODO: HANDLE UNEXPECTED NODE
        throw new Exception();
    }
}
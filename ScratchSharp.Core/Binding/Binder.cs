using System.Threading.Channels;
using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Core.Binding;

internal class Binder
{
    private readonly Dictionary<string, object> _variables;

    public Binder(Dictionary<string, object> variables)
    {
        _variables = variables;
    }

    public BoundExpression Bind(Expression syntax)
    {
        switch (syntax.Kind)
        {
            case SyntaxKind.ParanthesizedExpression:
                return BindParanthesizedExpression((ParanthesizedExpressionSyntax) syntax);
            case SyntaxKind.LiteralExpression:
                return BindLiteralExpression((LiteralExpressionSyntax)syntax);
            case SyntaxKind.NameExpression:
                return BindNameExpression((NameExpressionSyntax)syntax);
            case SyntaxKind.AssignmentExpression:
                return BindAssignmentExpression((AssignmentExpressionSyntax) syntax);
            case SyntaxKind.UnaryExpression:
                return BindUnaryExpression((UnaryExpressionSyntax)syntax);
            case SyntaxKind.BinaryExpression:
                return BindBinaryExpression((BinaryExpressionSyntax)syntax);
        }

        throw new Exception();
    }

    private BoundExpression BindParanthesizedExpression(ParanthesizedExpressionSyntax syntax) =>
        Bind(syntax.Expression);

    private BoundExpression BindNameExpression(NameExpressionSyntax syntax)
    {
        var name = syntax.Identifier.Text;
        if (!_variables.TryGetValue(name!, out var value))
        {
            //TODO: REPORT UNDEFINED VARIABLE (NAME)
            return new BoundLiteralExpression(0);
        }

        var type =  value.GetType();
        return new BoundVariableExpression(name!, type);
    }

    private BoundExpression BindAssignmentExpression(AssignmentExpressionSyntax syntax) => new BoundAssignmentExpression(syntax.Identifier.Text!, Bind(syntax.Expression));


    private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {
        var value = syntax.Value ?? 0;
        return new BoundLiteralExpression(value);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {
        var boundOperand = Bind(syntax.Operand);
        var boundOperator = BoundUnaryOperator.Bind(syntax.Operator.Kind, boundOperand.Type);
        if (boundOperator == null)
        {
            //TODO: HANDLE UNKNOWN UNARY OPERATOR FOR TYPE
            return boundOperand;
        }

        return new BoundUnaryExpression(boundOperator, boundOperand);
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {
        var boundLeft = Bind(syntax.Left);
        var boundRight = Bind(syntax.Right);
        var boundOperator = BoundBinaryOperator.Bind(syntax.Operator.Kind, boundLeft.Type, boundRight.Type);
        if (boundOperator == null)
        {
            //TODO: HANDLE UNKNOWN TYPE FOR BINARY OPERATOR
            return boundLeft;
        }
        return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
    }
}
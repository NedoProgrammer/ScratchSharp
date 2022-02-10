using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Core;

public class Parser
{
    private SyntaxToken[] _tokens;
    private int _position;
    public Parser(string text)
    {
        var tokens = new List<SyntaxToken>();
        var lexer = new Lexer(text);
        SyntaxToken token;
        do
        {
            token = lexer.Lex();
            if (token.Kind != SyntaxKind.Whitespace && token.Kind != SyntaxKind.Unknown)
                tokens.Add(token);
        } while (token.Kind != SyntaxKind.Eof);
        _tokens = tokens.ToArray();
    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        return index >= _tokens.Length ? _tokens[^1] : _tokens[index];
    }

    private SyntaxToken Current => Peek(0);

    public SyntaxTree Parse()
    {
        var expression = ParseExpression();
        var endOfFile = MatchToken(SyntaxKind.Eof);
        return new SyntaxTree(expression, endOfFile);
    }

    private Expression ParseBinaryExpression(int parentPrecedence = 0)
    {
        Expression left;
        
        var unaryPrecedence = Current.Kind.GetUnaryOperatorPrecedence();
        if (unaryPrecedence != 0 && unaryPrecedence >= parentPrecedence)
        {
            var op = Next();
            var operand = ParseBinaryExpression(unaryPrecedence);
            left = new UnaryExpressionSyntax(op, operand);
        }
        else
            left = ParsePrimaryExpression();

        while (true)
        {
            var precedence = Current.Kind.GetBinaryOperatorPrecedence();
            if (precedence == 0 || precedence <= parentPrecedence)
                break;
            var op = Next();
            var right = ParseBinaryExpression(precedence);
            left = new BinaryExpressionSyntax(left, op, right);
        }

        return left;
    }

    private Expression ParseExpression() => ParseAssignmentExpression();

    private Expression ParseAssignmentExpression()
    {
        if (Peek(0).Kind != SyntaxKind.Identifier || Peek(1).Kind != SyntaxKind.Assign) return ParseBinaryExpression();
        var identifier = Next();
        var op = Next();
        var right = ParseAssignmentExpression();
        return new AssignmentExpressionSyntax(identifier, op, right);
    }

    private SyntaxToken Next()
    {
        var current = Current;
        _position++;
        return current;
    }
    
    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
            return Next();
        //TODO: HANDLE "UNEXPETED TOKEN KIND, EXPECTED {KIND}"
        return new SyntaxToken(kind, Current.Position, null, null);
    }

    private Expression ParsePrimaryExpression()
    {
        switch (Current.Kind)
        {
            case SyntaxKind.LeftParanthesis:
            {
                var left = Next();
                var expression = ParseBinaryExpression();
                var right = MatchToken(SyntaxKind.RightParanthesis);
                return new ParanthesizedExpressionSyntax(left, expression, right);
            }
            case SyntaxKind.True or SyntaxKind.False:
            {
                var keyword = Next();
                var value = keyword.Kind == SyntaxKind.True;
                return new LiteralExpressionSyntax(keyword, value);
            }
            case SyntaxKind.Identifier:
            {
                var identifier = Next();
                return new NameExpressionSyntax(identifier);
            }
            default:
            {
                var number = MatchToken(SyntaxKind.Number);
                return new LiteralExpressionSyntax(number);
            }
        }
    }
}
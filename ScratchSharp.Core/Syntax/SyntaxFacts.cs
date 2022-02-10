namespace ScratchSharp.Core.Syntax;

public static class SyntaxFacts
{
    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    {
        switch (kind)
        {
            case SyntaxKind.Plus:
            case SyntaxKind.Minus:
            case SyntaxKind.Not:
                return 6;
            default:
                return 0;
        }
    }
    
    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        switch (kind)
        {
            case SyntaxKind.Multiply:
            case SyntaxKind.Divide:
                return 5;
            case SyntaxKind.Plus:
            case SyntaxKind.Minus:
                return 4;
            case SyntaxKind.Equals:
            case SyntaxKind.NotEquals:
                return 3;
            case SyntaxKind.And:
                return 2;
            case SyntaxKind.Or:
                return 1;
            default:
                return 0;
        }
    }

    public static SyntaxKind GetKeywordKind(string kind)
    {
        switch (kind)
        {
            case "true":
                return SyntaxKind.True;
            case "false":
                return SyntaxKind.False;
            default:
                return SyntaxKind.Identifier;
        }
    }
}
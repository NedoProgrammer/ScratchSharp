using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Core;

public class Lexer
{
    private readonly string _text;
    private int _position;
    public Lexer(string text)
    {
        _text = text;
    }

    private char Current => Peek(0);
    private char Lookahead => Peek(1);
    private void Next() => _position++;

    private char Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _text.Length)
            return '\0';
        return _text[index];
    }
    
    public SyntaxToken Lex()
    {
        if (_position >= _text.Length)
            return new SyntaxToken(SyntaxKind.Eof, _position, "\0", null);
        
        if (char.IsDigit(Current))
        {
            var start = _position;
            while(char.IsDigit(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            if (!int.TryParse(text, out var value))
            {
                //TODO: HANDLE FAILED TO PARSE AN INT ERROR.
            }
            return new SyntaxToken(SyntaxKind.Number, start, text, value);
        }
        
        if (char.IsWhiteSpace(Current))
        {
            var start = _position;
            while(char.IsWhiteSpace(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.Whitespace, start, text, null);
        }

        if (char.IsLetter(Current))
        {
            var start = _position;
            while(char.IsLetter(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            var kind = SyntaxFacts.GetKeywordKind(text);
            return new SyntaxToken(kind, start, text, null);
        }
        //TODO: HANDLE UNKNOWN TOKEN
        switch (Current)
        {
            case '+':
                return new SyntaxToken(SyntaxKind.Plus, _position++, "+", null);
            case '-':
                return new SyntaxToken(SyntaxKind.Minus, _position++, "-", null);
            case '*':
                return new SyntaxToken(SyntaxKind.Multiply, _position++, "*", null);
            case '/':
                return new SyntaxToken(SyntaxKind.Divide, _position++, "/", null);
            case '(':
                return new SyntaxToken(SyntaxKind.LeftParanthesis, _position++, "(", null);
            case ')':
                return new SyntaxToken(SyntaxKind.RightParanthesis, _position++, ")", null);
            case '!':
                return Lookahead == '=' ? new SyntaxToken(SyntaxKind.NotEquals, _position += 2, "!=", null) : new SyntaxToken(SyntaxKind.Not, _position++, "!", null);
            case '&':
                if (Lookahead == '&')
                    return new SyntaxToken(SyntaxKind.And, _position += 2, "&&", null);
                break;
            case '|':
                if (Lookahead == '|')
                    return new SyntaxToken(SyntaxKind.Or, _position += 2, "||", null);
                break;
            case '=':
                return Lookahead == '=' ? new SyntaxToken(SyntaxKind.Equals, _position += 2, "==", null) : new SyntaxToken(SyntaxKind.Assign, _position++, "=", null);
            default:
                throw new ArgumentOutOfRangeException(nameof(Current));
        }

        //how did you get here??
        throw new Exception();
    }
}
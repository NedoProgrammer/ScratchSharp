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
        
        var start = _position;
        if (char.IsDigit(Current))
        {
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
            while(char.IsWhiteSpace(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.Whitespace, start, text, null);
        }
        
        if (char.IsLetter(Current))
        {
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
            {
                if (Lookahead == '=')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.NotEquals, start, "!=", null);
                }

                _position++;
                return new SyntaxToken(SyntaxKind.Not, start, "!", null);
            }
            case '&':
                if (Lookahead == '&')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.And, start, "&&", null);
                }
                break;
            case '|':
                if (Lookahead == '|')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.Or, start, "||", null);
                }
                break;
            case '=':
            {
                if (Lookahead == '=')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.Equals, start, "==", null);
                }

                _position++;
                return new SyntaxToken(SyntaxKind.Assign, start, "=", null);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(Current));
        }

        //how did you get here??
        throw new Exception();
    }
}
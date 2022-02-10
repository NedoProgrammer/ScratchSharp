namespace ScratchSharp.Core.Syntax;

public enum SyntaxKind
{
    Unknown,
    Eof,
    
    Number,
    Whitespace,
    
    Plus,
    RightParanthesis,
    LeftParanthesis,
    Divide,
    Multiply,
    Minus,
    
    LiteralExpression,
    BinaryExpression,
    ParanthesizedExpression,
    UnaryExpression,
    NameExpression,
    AssignmentExpression,
    
    True,
    False,
    Identifier,
    Not,
    And,
    Or,
    Assign,
    Equals,
    NotEquals
}
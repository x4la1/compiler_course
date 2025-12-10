using System.Globalization;

using Microsoft.VisualBasic;

namespace Lexer;

public class Lexer
{
    private static readonly Dictionary<string, TokenType> Keywords = new()
    {
        {
            "if", TokenType.If
        },
        {
            "else", TokenType.Else
        },
        {
            "while", TokenType.While
        },
        {
            "continue", TokenType.Continue
        },
        {
            "break", TokenType.Break
        },
        {
            "print", TokenType.Print
        },
        {
            "input", TokenType.Input
        },
        {
            "int", TokenType.IntType
        },
        {
            "float", TokenType.FloatType
        },
        {
            "string", TokenType.StringType
        },
        {
            "bool", TokenType.BoolType
        },
        {
            "func", TokenType.Function
        },
        {
            "return", TokenType.Return
        },
        {
            "true", TokenType.True
        },
        {
            "false", TokenType.False
        },
        {
            "pi", TokenType.Pi
        },
        {
            "euler", TokenType.Euler
        },
    };

    private readonly TextScanner scanner;

    public Lexer(string str)
    {
        scanner = new TextScanner(str);
    }

    public Token ParseToken()
    {
        SkipWhiteSpacesAndComments();

        if (scanner.IsEnd())
        {
            return new Token(TokenType.EndOfFile);
        }

        char currentChar = scanner.Peek();
        if (char.IsLetter(currentChar) || currentChar == '_')
        {
            return ParseIdentifierOrKeyword();
        }

        if (currentChar == '"')
        {
            return ParseStringLiteral();
        }

        if (char.IsAsciiDigit(currentChar))
        {
            return ParseNumericLiteral();
        }

        return ParsePunctuator();
    }

    private void SkipWhiteSpacesAndComments()
    {
        do
        {
            SkipWhiteSpaces();
        }
        while (TryParseMultilineComment() || TryParseSingleLineComment());
    }

    private void SkipWhiteSpaces()
    {
        while (char.IsWhiteSpace(scanner.Peek()))
        {
            scanner.Advance();
        }
    }

    private Token ParseIdentifierOrKeyword()
    {
        string value = scanner.Peek().ToString();
        scanner.Advance();

        for (char c = scanner.Peek(); char.IsLetter(c) || c == '_' || char.IsAsciiDigit(c); c = scanner.Peek())
        {
            value += c;
            scanner.Advance();
        }

        if (Keywords.TryGetValue(value.ToLower(CultureInfo.InvariantCulture), out TokenType type))
        {
            return new Token(type);
        }

        return new Token(TokenType.Identifier, new TokenValue(value));
    }

    private bool TryParseMultilineComment()
    {
        if (scanner.Peek() == '/' && scanner.Peek(1) == '*')
        {
            do
            {
                scanner.Advance();
            }
            while (!(scanner.Peek() == '*' && scanner.Peek(1) == '/'));

            scanner.Advance();
            scanner.Advance();
            return true;
        }

        return false;
    }

    private bool TryParseSingleLineComment()
    {
        if (scanner.Peek() == '/' && scanner.Peek(1) == '/')
        {
            do
            {
                scanner.Advance();
            }
            while (scanner.Peek() != '\n' && scanner.Peek() != '\r' && !scanner.IsEnd());

            return true;
        }

        return false;
    }

    private Token ParseStringLiteral()
    {
        scanner.Advance();

        string content = "";
        while (scanner.Peek() != '"')
        {
            if (scanner.IsEnd())
            {
                return new Token(TokenType.Error, new TokenValue(content));
            }

            if (TryParseStringLiteralEscapeSequence(out char unescaped))
            {
                content += unescaped;
            }
            else
            {
                content += scanner.Peek();
                scanner.Advance();
            }
        }

        scanner.Advance();
        return new Token(TokenType.StringLiteral, new TokenValue(content));
    }

    private bool TryParseStringLiteralEscapeSequence(out char unescaped)
    {
        if (scanner.Peek() != '\\')
        {
            unescaped = '\0';
            return false;
        }

        scanner.Advance();
        switch (scanner.Peek())
        {
            case '\\': unescaped = '\\'; break;
            case '\'': unescaped = '\''; break;
            case 'n': unescaped = '\n'; break;
            default:
                unescaped = '\0';
                return false;
        }

        scanner.Advance();
        return true;
    }

    private Token ParseNumericLiteral()
    {
        decimal value = GetDigitValue(scanner.Peek());
        scanner.Advance();

        for (char c = scanner.Peek(); char.IsAsciiDigit(c); c = scanner.Peek())
        {
            value = value * 10 + GetDigitValue(c);
            scanner.Advance();
        }

        if (scanner.Peek() == '.')
        {
            scanner.Advance();
            decimal factor = 0.1m;
            for (char c = scanner.Peek(); char.IsAsciiDigit(c); c = scanner.Peek())
            {
                scanner.Advance();
                value += factor * GetDigitValue(c);
                factor *= 0.1m;
            }
        }

        return new Token(TokenType.NumericLiteral, new TokenValue(value));

        static int GetDigitValue(char c)
        {
            return c - '0';
        }
    }

    private Token ParsePunctuator()
    {
        char currentChar = scanner.Peek();

        switch (currentChar)
        {
            case '+':
                scanner.Advance();
                return new Token(TokenType.PlusSign);
            case '-':
                scanner.Advance();
                return new Token(TokenType.MinusSign);
            case '*':
                scanner.Advance();
                return new Token(TokenType.MultiplySign);
            case '/':
                scanner.Advance();
                return new Token(TokenType.DivideSign);
            case '%':
                scanner.Advance();
                return new Token(TokenType.ModuloSign);
            case '^':
                scanner.Advance();
                return new Token(TokenType.ExponentiationSign);
            case '@':
                scanner.Advance();
                return new Token(TokenType.ExactDivideSign);

            case '=':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.EqualSign);
                }

                return new Token(TokenType.AssignSign);
            case '!':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.NotEqualSign);
                }

                return new Token(TokenType.NotSign);
            case '<':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.LessOrEqualSign);
                }

                return new Token(TokenType.LessSign);
            case '>':
                scanner.Advance();
                if (scanner.Peek() == '=')
                {
                    scanner.Advance();
                    return new Token(TokenType.GreaterOrEqualSign);
                }

                return new Token(TokenType.GreaterSign);
            case '&':
                scanner.Advance();
                if (scanner.Peek() == '&')
                {
                    scanner.Advance();
                    return new Token(TokenType.And);
                }

                return new Token(TokenType.Error, new TokenValue(scanner.Peek()));
            case '|':
                scanner.Advance();
                if (scanner.Peek() == '|')
                {
                    scanner.Advance();
                    return new Token(TokenType.Or);
                }

                return new Token(TokenType.Error, new TokenValue(scanner.Peek()));
            case ',':
                scanner.Advance();
                return new Token(TokenType.Comma);
            case ';':
                scanner.Advance();
                return new Token(TokenType.Semicolon);
            case '(':
                scanner.Advance();
                return new Token(TokenType.OpenParenthesis);
            case ')':
                scanner.Advance();
                return new Token(TokenType.CloseParenthesis);
            case '{':
                scanner.Advance();
                return new Token(TokenType.OpenBrace);
            case '}':
                scanner.Advance();
                return new Token(TokenType.CloseBrace);
        }

        scanner.Advance();
        return new Token(TokenType.Error, new TokenValue(currentChar.ToString()));
    }
}

using ExampleLib.UnitTests.Helpers;

namespace Lexer.UnitTests;

public class LexerTests
{
    [Theory]
    [MemberData(nameof(GetTokenizeData))]
    public void CanTokenize(string text, List<Token> expected)
    {
        List<Token> actual = Tokenize(text);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GetStatistics))]
    public void CanCollectStatistics(string text, string expected)
    {
        using TempFile file = TempFile.Create(text);
        string actual = LexicalStats.CollectFromFile(file.Path);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<string, List<Token>> GetTokenizeData()
    {
        return new TheoryData<string, List<Token>>
        {
            {
                "int x;",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "int x = 1;",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(1)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "int x = -10;",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.MinusSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(10)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "string x = \"Hello World!\";",
                [
                    new Token(TokenType.StringType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.StringLiteral, new TokenValue("Hello World!")),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "float x = 1.5;",
                [
                    new Token(TokenType.FloatType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(1.5m)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "bool x = true;",
                [
                    new Token(TokenType.BoolType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.True),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "bool x = false;",
                [
                    new Token(TokenType.BoolType),
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.False),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "x = 1 + 2;",
                [
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(1)),
                    new Token(TokenType.PlusSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "x = 2 - 1;",
                [
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.MinusSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(1)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "x  = 2 * 2;",
                [
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.MultiplySign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "x  = 2 / 2;",
                [
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.DivideSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "x = 5 % 2;",
                [
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(5)),
                    new Token(TokenType.ModuloSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "x = 5 ^ 2;",
                [
                    new Token(TokenType.Identifier, new TokenValue("x")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(5)),
                    new Token(TokenType.ExponentiationSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(2)),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "while (a > b && a < b || a >= b || a <= b && a == b || a != b) {}",
                [
                    new Token(TokenType.While),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.GreaterSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.And),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.LessSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Or),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.GreaterOrEqualSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Or),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.LessOrEqualSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.And),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.EqualSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Or),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.NotEqualSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.OpenBrace),
                    new Token(TokenType.CloseBrace),
                ]
            },
            {
                "while (true) {break; continue;}",
                [
                    new Token(TokenType.While),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.True),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.OpenBrace),
                    new Token(TokenType.Break),
                    new Token(TokenType.Semicolon),
                    new Token(TokenType.Continue),
                    new Token(TokenType.Semicolon),
                    new Token(TokenType.CloseBrace),
                ]
            },
            {
                "int func aplusb(int a, int b) { return a + b; };",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Function),
                    new Token(TokenType.Identifier, new TokenValue("aplusb")),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.Comma),
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.OpenBrace),
                    new Token(TokenType.Return),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.PlusSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Semicolon),
                    new Token(TokenType.CloseBrace),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "if (a > b) { a + 1; } else { a - 1; }",
                [
                    new Token(TokenType.If),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.GreaterSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.OpenBrace),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.PlusSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(1)),
                    new Token(TokenType.Semicolon),
                    new Token(TokenType.CloseBrace),
                    new Token(TokenType.Else),
                    new Token(TokenType.OpenBrace),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.MinusSign),
                    new Token(TokenType.NumericLiteral, new TokenValue(1)),
                    new Token(TokenType.Semicolon),
                    new Token(TokenType.CloseBrace),
                ]
            },
            {
                "input(a);",
                [
                    new Token(TokenType.Input),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "print(a);",
                [
                    new Token(TokenType.Print),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "FuNc abc();",
                [
                    new Token(TokenType.Function),
                    new Token(TokenType.Identifier, new TokenValue("abc")),
                    new Token(TokenType.OpenParenthesis),
                    new Token(TokenType.CloseParenthesis),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "StRing a;",
                [
                    new Token(TokenType.StringType),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.Semicolon),
                ]
            },
            {
                "int _Abc1;",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("_Abc1")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "int a;",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "int _1abc_;",
                [
                    new Token(TokenType.IntType),
                    new Token(TokenType.Identifier, new TokenValue("_1abc_")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "a = +b;",
                [
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.PlusSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "a = -b;",
                [
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.MinusSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "a = !b;",
                [
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.NotSign),
                    new Token(TokenType.Identifier, new TokenValue("b")),
                    new Token(TokenType.Semicolon)
                ]
            },
            {
                "a = \"\\\"Hello\\\" \\n \\\\World\\\\\"",
                [
                    new Token(TokenType.Identifier, new TokenValue("a")),
                    new Token(TokenType.AssignSign),
                    new Token(TokenType.StringLiteral, new TokenValue("\"Hello\" \n \\World\\"))
                ]
            },
            {
                "//комментарий",
                []
            },
            {
                "/*комментарий*/",
                []
            },
        };
    }

    public static TheoryData<string, string> GetStatistics()
    {
        return new TheoryData<string, string>
        {
            {
                @"string name;
                print(""Enter your name: "");
                input(name);
                if (name == """") {
                    print(""Hello, stranger!"");
                } else {
                    print(""Hello, "", name, ""!"");
                ",
                """
                keywords: 7
                identifiers: 4
                number literals: 0
                string literals: 5
                operators: 1
                other lexemes: 20
                """
            },
            {
                @"/*
                тута
                кароче
                код
                эээ
                */
                int n;
                print(""Enter n: "");
                input(n);
                if (n < 0) {
                    print(""Error: n must be non-negative.""); //чето выводит
                } else {
                    int result = 1;
	                int i = 1;
	                while(i <= n)
	                {
		                result = result * i;
		                i = i + 1;
	                }
                    print(""Factorial: "", result);
                }
                ",
                """
                keywords: 10
                identifiers: 13
                number literals: 4
                string literals: 3
                operators: 8
                other lexemes: 28
                """
            },
        };
    }

    private List<Token> Tokenize(string str)
    {
        List<Token> results = [];
        Lexer lexer = new(str);

        for (Token t = lexer.ParseToken(); t.Type != TokenType.EndOfFile; t = lexer.ParseToken())
        {
            results.Add(t);
        }

        return results;
    }
}
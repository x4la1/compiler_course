using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer;

public static class LexicalStats
{
    private static readonly Dictionary<TokenType, string> TokenTypeToCategory = new()
    {
        [TokenType.If] = "keywords",
        [TokenType.Else] = "keywords",
        [TokenType.While] = "keywords",
        [TokenType.Print] = "keywords",
        [TokenType.Input] = "keywords",
        [TokenType.IntType] = "keywords",
        [TokenType.FloatType] = "keywords",
        [TokenType.StringType] = "keywords",
        [TokenType.BoolType] = "keywords",
        [TokenType.Function] = "keywords",
        [TokenType.Return] = "keywords",
        [TokenType.True] = "keywords",
        [TokenType.False] = "keywords",

        [TokenType.PlusSign] = "operators",
        [TokenType.MinusSign] = "operators",
        [TokenType.MultiplySign] = "operators",
        [TokenType.DivideSign] = "operators",
        [TokenType.ModuloSign] = "operators",
        [TokenType.ExponentiationSign] = "operators",
        [TokenType.EqualSign] = "operators",
        [TokenType.NotEqualSign] = "operators",
        [TokenType.LessSign] = "operators",
        [TokenType.GreaterSign] = "operators",
        [TokenType.LessOrEqualSign] = "operators",
        [TokenType.GreaterOrEqualSign] = "operators",
        [TokenType.NotSign] = "operators",
        [TokenType.And] = "operators",
        [TokenType.Or] = "operators",
        [TokenType.AssignSign] = "operators",

        [TokenType.Identifier] = "identifiers",
        [TokenType.NumericLiteral] = "number literals",
        [TokenType.StringLiteral] = "string literals",

        [TokenType.Comma] = "other lexemes",
        [TokenType.Semicolon] = "other lexemes",
        [TokenType.OpenParenthesis] = "other lexemes",
        [TokenType.CloseParenthesis] = "other lexemes",
        [TokenType.OpenBrace] = "other lexemes",
        [TokenType.CloseBrace] = "other lexemes",
        [TokenType.Error] = "other lexemes",
    };

    private static readonly Dictionary<string, int> Statistics = new()
    {
        { "keywords", 0 },
        { "identifiers", 0 },
        { "number literals", 0 },
        { "string literals", 0 },
        { "operators", 0 },
        { "other lexemes", 0 },
    };

    public static string CollectFromFile(string path)
    {
        ResetStatistics();
        string contents;
        try
        {
            contents = File.ReadAllText(path);
        }
        catch
        {
            throw new ArgumentException("Couldn't read the file");
        }

        Lexer lexer = new(contents);
        while (true)
        {
            Token token = lexer.ParseToken();
            if (token.Type == TokenType.EndOfFile)
            {
                break;
            }

            Statistics[TokenTypeToCategory[token.Type]]++;
        }

        return $"""
            keywords: {Statistics["keywords"]}
            identifiers: {Statistics["identifiers"]}
            number literals: {Statistics["number literals"]}
            string literals: {Statistics["string literals"]}
            operators: {Statistics["operators"]}
            other lexemes: {Statistics["other lexemes"]}
            """;
    }

    private static void ResetStatistics()
    {
        Statistics["keywords"] = 0;
        Statistics["identifiers"] = 0;
        Statistics["number literals"] = 0;
        Statistics["string literals"] = 0;
        Statistics["operators"] = 0;
        Statistics["other lexemes"] = 0;
    }
}
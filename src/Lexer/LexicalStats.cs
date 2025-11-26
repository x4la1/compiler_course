using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer;

public static class LexicalStats
{
    private static readonly string Keywords = "keywords";
    private static readonly string Operators = "operators";
    private static readonly string Identifiers = "identifiers";
    private static readonly string NumberLiterals = "number literals";
    private static readonly string StringLiterals = "string literals";
    private static readonly string OtherLexemes = "other lexemes";

    private static readonly Dictionary<TokenType, string> TokenTypeToCategory = new()
    {
        [TokenType.If] = Keywords,
        [TokenType.Else] = Keywords,
        [TokenType.While] = Keywords,
        [TokenType.Print] = Keywords,
        [TokenType.Input] = Keywords,
        [TokenType.IntType] = Keywords,
        [TokenType.FloatType] = Keywords,
        [TokenType.StringType] = Keywords,
        [TokenType.BoolType] = Keywords,
        [TokenType.Function] = Keywords,
        [TokenType.Return] = Keywords,
        [TokenType.True] = Keywords,
        [TokenType.False] = Keywords,
        [TokenType.Break] = Keywords,
        [TokenType.Continue] = Keywords,

        [TokenType.PlusSign] = Operators,
        [TokenType.MinusSign] = Operators,
        [TokenType.MultiplySign] = Operators,
        [TokenType.DivideSign] = Operators,
        [TokenType.ModuloSign] = Operators,
        [TokenType.ExponentiationSign] = Operators,
        [TokenType.EqualSign] = Operators,
        [TokenType.NotEqualSign] = Operators,
        [TokenType.LessSign] = Operators,
        [TokenType.GreaterSign] = Operators,
        [TokenType.LessOrEqualSign] = Operators,
        [TokenType.GreaterOrEqualSign] = Operators,
        [TokenType.NotSign] = Operators,
        [TokenType.And] = Operators,
        [TokenType.Or] = Operators,
        [TokenType.AssignSign] = Operators,

        [TokenType.Identifier] = Identifiers,
        [TokenType.NumericLiteral] = NumberLiterals,
        [TokenType.StringLiteral] = StringLiterals,

        [TokenType.Comma] = OtherLexemes,
        [TokenType.Semicolon] = OtherLexemes,
        [TokenType.OpenParenthesis] = OtherLexemes,
        [TokenType.CloseParenthesis] = OtherLexemes,
        [TokenType.OpenBrace] = OtherLexemes,
        [TokenType.CloseBrace] = OtherLexemes,
        [TokenType.Error] = OtherLexemes,
    };

    public static string CollectFromFile(string path)
    {
        Dictionary<string, int> statistics = new()
        {
            { Keywords, 0 },
            { Identifiers, 0 },
            { NumberLiterals, 0 },
            { StringLiterals, 0 },
            { Operators, 0 },
            { OtherLexemes, 0 },
        };

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

            statistics[TokenTypeToCategory[token.Type]]++;
        }

        return $"""
            {Keywords}: {statistics[Keywords]}
            {Identifiers}: {statistics[Identifiers]}
            {NumberLiterals}: {statistics[NumberLiterals]}
            {StringLiterals}: {statistics[StringLiterals]}
            {Operators}: {statistics[Operators]}
            {OtherLexemes}: {statistics[OtherLexemes]}
            """;
    }
}
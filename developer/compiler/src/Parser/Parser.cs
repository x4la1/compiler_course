using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Execution;

using Lexer;

namespace Parser;

/// <summary>
/// Выполняет синтаксический разбор.
/// Грамматика языка описана в файле `docs/specification/expressions-grammar.md`.
/// </summary>
public class Parser
{
    private readonly Context context;
    private readonly IEnvironment environment;
    private readonly TokenStream tokens;

    public Parser(Context context, IEnvironment environment, string code)
    {
        this.context = context;
        this.environment = environment;
        this.tokens = new TokenStream(code);
    }

    /// <summary>
    ///  Разбирает программу
    ///  Правила:
    ///     program = statement, {statement};.
    /// </summary>
    public void ParseProgram()
    {
        while (tokens.Peek().Type != TokenType.EndOfFile)
        {
            ParseStatement();
        }
    }

    /// <summary>
    ///  Разбирает интрукцию
    ///  Правила:
    ///     statement = variable_declaration
    ///         | assignment_statement
    ///         | print_statement
    ///         | input_statement
    ///         | ";" ;  (* пустая инструкция *).
    /// </summary>
    private void ParseStatement()
    {
        switch (tokens.Peek().Type)
        {
            case TokenType.FloatType:
                ParseVarDeclarationExpression();
                break;
            case TokenType.Identifier:
                ParseAssignmentStatement();
                break;
            case TokenType.Print:
                ParsePrintStatement();
                break;
            case TokenType.Input:
                ParseInputStatement();
                break;
            case TokenType.Semicolon:
                tokens.Advance();
                break;
        }
    }

    /// <summary>
    ///  Разбирает выражение объявления переменной
    ///  Правила:
    ///     variable_declaration = "float" , identifier , [ "=" , expression ].
    /// </summary>
    private void ParseVarDeclarationExpression()
    {
        Match(TokenType.FloatType);

        Token identifier = Match(TokenType.Identifier);
        decimal initialValue = 0;
        if (tokens.Peek().Type == TokenType.EqualSign)
        {
            Match(TokenType.EqualSign);
            initialValue = ParseExpression();
        }

        context.DefineVariable(identifier.Value!.ToString(), initialValue);
        Match(TokenType.Semicolon);
    }

    /// <summary>
    ///  Разбирает выражение присваивания значения переменной
    ///  Правила:
    ///     assignment_statement = identifier , "=" , expression.
    /// </summary>
    private void ParseAssignmentStatement()
    {
        Token identifier = Match(TokenType.Identifier);
        Match(TokenType.AssignSign);
        context.AssignVariable(identifier.Value!.ToString(), ParseExpression());
        Match(TokenType.Semicolon);
    }

    /// <summary>
    ///  Разбирает выражение печатания значения переменной в консоль
    ///  Правила:
    ///     print_statement = "print" , "(" , [ expression , { "," , expression } ] , ")".
    /// </summary>
    private void ParsePrintStatement()
    {
        Match(TokenType.Print);
        Match(TokenType.OpenParenthesis);

        if (tokens.Peek().Type != TokenType.CloseParenthesis)
        {
            decimal value = ParseExpression();
            environment.WriteNumber(value);

            while (tokens.Peek().Type == TokenType.Comma)
            {
                Match(TokenType.Comma);
                value = ParseExpression();
                environment.WriteNumber(value);
            }
        }

        Match(TokenType.CloseParenthesis);
        Match(TokenType.Semicolon);
    }

    /// <summary>
    ///  Разбирает выражение считывания из консоли в переменную
    ///  Правила:
    ///     input_statement = "input" , "(" , identifier , ")".
    /// </summary>
    private void ParseInputStatement()
    {
        Match(TokenType.Input);
        Match(TokenType.OpenParenthesis);

        Token identifier = Match(TokenType.Identifier);
        Match(TokenType.CloseParenthesis);
        Match(TokenType.Semicolon);

        context.AssignVariable(identifier.Value!.ToString(), environment.ReadNumber());
    }

    /// <summary>
    ///  Разбирает выражение
    ///  Правила:
    ///     expression = or_expr.
    /// </summary>
    private decimal ParseExpression()
    {
        return ParseOrExpression();
    }

    /// <summary>
    ///  Разбирает выражение с логическим ИЛИ
    ///  Правила:
    ///     or_expr = and_expr , { "||" , and_expr } .
    /// </summary>
    private decimal ParseOrExpression()
    {
        decimal value = ParseAndExpression();
        while (tokens.Peek().Type == TokenType.Or)
        {
            tokens.Advance();
            value = (value != 0 || ParseAndExpression() != 0) ? 1 : 0;
        }

        return value;
    }

    /// <summary>
    ///  Разбирает выражение с логическим И
    ///  Правила:
    ///     and_expr = equality_expr , { "&&" , equality_expr }.
    /// </summary>
    private decimal ParseAndExpression()
    {
        decimal value = ParseEqualityExpression();
        while (tokens.Peek().Type == TokenType.And)
        {
            tokens.Advance();
            value = (value != 0 && ParseEqualityExpression() != 0) ? 1 : 0;
        }

        return value;
    }

    /// <summary>
    ///  Разбирает выражение с сравнением  ==, !=
    ///  Правила:
    ///     equality_expr = relational_expr , { ("==" | "!=") , relational_expr }.
    /// </summary>
    private decimal ParseEqualityExpression()
    {
        decimal value = ParseCompareExpression();
        while (true)
        {
            switch (tokens.Peek().Type)
            {
                case TokenType.EqualSign:
                    tokens.Advance();
                    value = (value == ParseCompareExpression()) ? 1 : 0;
                    break;
                case TokenType.NotEqualSign:
                    tokens.Advance();
                    value = (value != ParseCompareExpression()) ? 1 : 0;
                    break;
                default:
                    return value;
            }
        }
    }

    /// <summary>
    ///  Разбирает выражение с сравнением >, <, >=, <=
    ///  Правила:
    ///     relational_expr = additive_expr , { ("<" | "<=" | ">" | ">=") , additive_expr }.
    /// </summary>
    private decimal ParseCompareExpression()
    {
        decimal value = ParseAdditiveExpression();
        while (true)
        {
            switch (tokens.Peek().Type)
            {
                case TokenType.LessSign:
                    tokens.Advance();
                    value = (value < ParseAdditiveExpression()) ? 1 : 0;
                    break;
                case TokenType.LessOrEqualSign:
                    tokens.Advance();
                    value = (value <= ParseAdditiveExpression()) ? 1 : 0;
                    break;
                case TokenType.GreaterSign:
                    tokens.Advance();
                    value = (value > ParseAdditiveExpression()) ? 1 : 0;
                    break;
                case TokenType.GreaterOrEqualSign:
                    tokens.Advance();
                    value = (value >= ParseAdditiveExpression()) ? 1 : 0;
                    break;
                default:
                    return value;
            }
        }
    }

    /// <summary>
    ///  Разбирает аддитивное выражение
    ///  Правила:
    ///     additive_expr = multiplicative_expr , { ("+" | "-") , multiplicative_expr }.
    /// </summary>
    private decimal ParseAdditiveExpression()
    {
        decimal value = ParseMultiplicativeExpression();
        while (true)
        {
            switch (tokens.Peek().Type)
            {
                case TokenType.PlusSign:
                    tokens.Advance();
                    value += ParseMultiplicativeExpression();
                    break;
                case TokenType.MinusSign:
                    tokens.Advance();
                    value -= ParseMultiplicativeExpression();
                    break;
                default:
                    return value;
            }
        }
    }

    /// <summary>
    ///  Разбирает мультипликативное выражение
    ///  Правила:
    ///     multiplicative_expr = unary_expr , { ("*" | "/" | "%" | "@") , unary_expr }.
    /// </summary>
    private decimal ParseMultiplicativeExpression()
    {
        decimal value = ParseUnaryExpression();
        while (true)
        {
            switch (tokens.Peek().Type)
            {
                case TokenType.MultiplySign:
                    tokens.Advance();
                    value *= ParseUnaryExpression();
                    break;
                case TokenType.DivideSign:
                    tokens.Advance();
                    value /= ParseUnaryExpression();
                    break;
                case TokenType.ModuloSign:
                    tokens.Advance();
                    value %= ParseUnaryExpression();
                    break;
                case TokenType.ExactDivideSign:
                    tokens.Advance();
                    value = Math.Truncate(value / ParseUnaryExpression());
                    break;
                default:
                    return value;
            }
        }
    }

    /// <summary>
    ///  Разбирает унарное выражение
    ///  Правила:
    ///     unary_expr = power_expr | ("+" | "-" | "!") , unary_expr ;.
    /// </summary>
    private decimal ParseUnaryExpression()
    {
        switch (tokens.Peek().Type)
        {
            case TokenType.PlusSign:
                tokens.Advance();
                return ParseUnaryExpression();
            case TokenType.MinusSign:
                tokens.Advance();
                return -ParseUnaryExpression();
            case TokenType.NotSign:
                tokens.Advance();
                decimal value = ParseUnaryExpression();
                return value == 0m ? 1m : 0m;
            default:
                return ParsePowerExpression();
        }
    }

    /// <summary>
    ///  Разбирает выражение возведения в степень.
    ///  Правила:
    ///     power_expr = primary_expr , [ "^" , power_expr ].
    /// </summary>
    private decimal ParsePowerExpression()
    {
        decimal value = ParsePrimaryExpression();
        if (tokens.Peek().Type == TokenType.ExponentiationSign)
        {
            tokens.Advance();
            value = (decimal)Math.Pow((double)value, (double)ParsePowerExpression());
        }

        return value;
    }

    /// <summary>
    ///  Разбирает главную часть выражения.
    ///  Правила:
    ///     primary_expr = number_literal | identifier | "(" , expression , ")".
    /// </summary>
    private decimal ParsePrimaryExpression()
    {
        Token t = tokens.Peek();
        switch (t.Type)
        {
            case TokenType.NumericLiteral:
                tokens.Advance();
                return t.Value!.ToDecimal();
            case TokenType.Euler:
            case TokenType.Pi:
                decimal constant = ParseConstant();
                tokens.Advance();
                return constant;
            case TokenType.OpenParenthesis:
                tokens.Advance();
                decimal value = ParseExpression();
                Match(TokenType.CloseParenthesis);
                return value;
            case TokenType.Identifier:
                tokens.Advance();
                return context.GetValue(t.Value!.ToString());
            default:
                throw new UnexpectedLexemeException(t.Type, t);
        }
    }

    private decimal ParseConstant()
    {
        Token t = tokens.Peek();
        return t.Type switch
        {
            TokenType.Pi => 3.14159265358m,
            TokenType.Euler => 2.71828182846m,
            _ => throw new UnexpectedLexemeException(t.Type, t)
        };
    }

    private Token Match(TokenType expected)
    {
        Token t = tokens.Peek();
        if (t.Type != expected)
        {
            throw new UnexpectedLexemeException(expected, t);
        }

        tokens.Advance();
        return t;
    }
}
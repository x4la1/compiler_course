using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer;

public class LexicalStats
{
    private Lexer lexer = new("");

    private Dictionary<string, int> statistics = new()
    {
        {"keywords", 0},
        {"identifiers", 0 },
        {"number literals", 0},
        {"string literals", 0 },
        {"operators", 0 },
        {"other lexemes", 0 },
    };

    public string CollectFromFile(string path)
    {
        string contents = File.ReadAllText(path); // отладить
        lexer = new Lexer(contents);

        while (true)
        {
            Token token = lexer.ParseToken();
            if (token.Type == TokenType.EndOfFile)
            {
                break;
            }

            switch (token.Type)
            {
                case TokenType.If:
                    statistics["keywords"]++;
                    break;
                case TokenType.Else:
                    statistics["keywords"]++;
                    break;
                case TokenType.While:
                    statistics["keywords"]++;
                    break;
                case TokenType.Print:
                    statistics["keywords"]++;
                    break;
                case TokenType.Input:
                    statistics["keywords"]++;
                    break;
                case TokenType.IntType:
                    statistics["keywords"]++;
                    break;
                case TokenType.FloatType:
                    statistics["keywords"]++;
                    break;
                case TokenType.StringType:
                    statistics["keywords"]++;
                    break;
                case TokenType.BoolType:
                    statistics["keywords"]++;
                    break;
                case TokenType.Function:
                    statistics["keywords"]++;
                    break;
                case TokenType.Return:
                    statistics["keywords"]++;
                    break;
                case TokenType.True:
                    statistics["keywords"]++;
                    break;
                case TokenType.False:
                    statistics["keywords"]++;
                    break;

                case TokenType.Identifier:
                    statistics["identifiers"]++;
                    break;
                case TokenType.NumericLiteral:
                    statistics["number literals"]++;
                    break;
                case TokenType.StringLiteral:
                    statistics["string literals"]++;
                    break;

                case TokenType.PlusSign:
                    statistics["operators"]++;
                    break;
                case TokenType.MinusSign:
                    statistics["operators"]++;
                    break;
                case TokenType.MultiplySign:
                    statistics["operators"]++;
                    break;
                case TokenType.DivideSign:
                    statistics["operators"]++;
                    break;
                case TokenType.ModuloSign:
                    statistics["operators"]++;
                    break;
                case TokenType.ExponentiationSign:
                    statistics["operators"]++;
                    break;
                case TokenType.EqualSign:
                    statistics["operators"]++;
                    break;
                case TokenType.NotEqualSign:
                    statistics["operators"]++;
                    break;
                case TokenType.LessSign:
                    statistics["operators"]++;
                    break;
                case TokenType.GreaterSign:
                    statistics["operators"]++;
                    break;
                case TokenType.LessOrEqualSign:
                    statistics["operators"]++;
                    break;
                case TokenType.GreaterOrEqualSign:
                    statistics["operators"]++;
                    break;
                case TokenType.NotSign:
                    statistics["operators"]++;
                    break;
                case TokenType.And:
                    statistics["operators"]++;
                    break;
                case TokenType.Or:
                    statistics["operators"]++;
                    break;
                case TokenType.AssignSign:
                    statistics["operators"]++;
                    break;
                default:
                    statistics["othere lexemes"]++;
                    break;
            }
        }

        return "";
    }
}


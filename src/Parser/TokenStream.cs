using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lexer;

namespace Parser;

public class TokenStream
{
    private readonly Lexer.Lexer lexer;
    private Token nextToken;

    public TokenStream(string text)
    {
        lexer = new Lexer.Lexer(text);
        nextToken = lexer.ParseToken();
    }

    public Token Peek()
    {
        return nextToken;
    }

    public void Advance()
    {
        nextToken = lexer.ParseToken();
    }
}
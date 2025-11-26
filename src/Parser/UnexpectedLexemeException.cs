using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lexer;

namespace Parser;

#pragma warning disable RCS1194
public class UnexpectedLexemeException : Exception
{
    public UnexpectedLexemeException(TokenType expected, Token actual)
        : base($"Unexpected lexeme {actual} where expected {expected}")
    {
    }
}
#pragma warning restore RCS1194
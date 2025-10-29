using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer;

public class TextScanner(string str)
{
    private readonly string text = str;
    private int position;

    public char Peek(int n = 0)
    {
        int position = this.position + n;
        return position >= text.Length ? '\0' : text[position];
    }

    public void Advance()
    {
        position++;
    }

    public bool IsEnd()
    {
        return position >= text.Length;
    }
}
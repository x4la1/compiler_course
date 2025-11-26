using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleLib.UnitTests.Helpers;

namespace Lexer.UnitTests;

public class LexicalStatsTests
{
    [Theory]
    [MemberData(nameof(GetStatistics))]
    public void CanCollectStatistics(string text, string expected)
    {
        using TempFile file = TempFile.Create(text);
        string actual = LexicalStats.CollectFromFile(file.Path);
        Assert.Equal(expected, actual);
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
}
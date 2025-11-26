using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Execution;

namespace Parser.UnitTests;

public class ParseTopLevelStatementsTests
{
    private const int Precision = 5;

    private readonly Context context;
    private readonly FakeEnvironment environment;

    public ParseTopLevelStatementsTests()
    {
        context = new Context();
        environment = new FakeEnvironment();
    }

    [Theory]
    [MemberData(nameof(GetCanParseTopLevelStatementsData))]
    public void CanParseTopLevelStatements(string code, List<decimal> expected)
    {
        Parser parser = new(context, environment, code);
        parser.ParseProgram();

        IReadOnlyList<decimal> actual = environment.Results;
        for (int i = 0, iMax = Math.Min(expected.Count, actual.Count); i < iMax; ++i)
        {
            Assert.Equal(expected[i], actual[i], Precision);
        }

        if (expected.Count != actual.Count)
        {
            Assert.Fail(
                $"Actual results count does not match expected. Expected: {expected.Count}, Actual: {actual.Count}."
            );
        }
    }

    public static TheoryData<string, List<decimal>> GetCanParseTopLevelStatementsData()
    {
        return new TheoryData<string, List<decimal>>
        {
            {
                "float x; print(x);", [0]
            },
            {
                "float y = 10; print(y);", [10]
            },
            {
                "float z = 2 * 5; print(z);", [10]
            },
            {
                "float z; z = 2 * 5; print(z);", [10]
            },
            {
                "print(52);", [52]
            },
            {
                "float x = Pi; print(x);", [3.141592m]
            },
            {
                "print(2 ^ 3);", [8]
            },
            {
                "print(2 ^ 3, 4 * 4);", [8, 16]
            },
            {
                "print();", []
            },
        };
    }
}
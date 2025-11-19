using Execution;

namespace Parser.UnitTests;

public class ParserTests
{
    private const int Precision = 5;

    private readonly Context context;
    private readonly FakeEnvironment environment;

    public ParseTopLevelStatementsTest()
    {
        context = new Context();
        environment = new FakeEnvironment();
    }

    [Theory]
    [MemberData(nameof(GetExpressionTestData))]
    public void CanParseExpressions(string code, decimal expected)
    {
        decimal actual = Parser.;
        Assert.Equal(expected, actual, Precision);
    }

    public static TheoryData<string, decimal> GetExpressionTestData() => new()
    {
        {
            "+5", 5
        },
        {
            "-10", -10
        },
        {
            "--5", 5
        },
        {
            "!!5", 1
        },
        {
            "!0", 1
        },
        {
            "!1", 0
        },
        {
            "2 ^ 3", 8
        },
        {
            "2 ^ 3 ^ 2", 512
        },
        {
            "-2 ^ 3", -8
        },
        {
            "6 * 2", 12
        },
        {
            "9 / 2", 4.5m
        },
        {
            "10 % 3", 1
        },
        {
            "10 @ 3", 3
        },
        {
            "8 / 2 / 2", 2
        },
        {
            "1 + 2 * 3", 7
        },
        {
            "1 - 2 - 3", -4
        },
        {
            "1 + 2 < 5", 1
        },
        {
            "5 <= 5", 1
        },
        {
            "10 > 5", 1
        },
        {
            "10 >= 10", 1
        },
        {
            "5 < 5", 0
        },
        {
            "(5 < 10) == 1", 1
        },
        {
            "(5 < 10) != 1", 0
        },
        {
            "(1 == 1) && (2 != 3)", 1
        },
        {
            "1 && 0 || 1", 1
        },
        {
            "1 + 2 * 3 ^ 2 - 4 / 2", 17
        },
        {
            "(1 + 2) * 3", 9
        },
        {
            "Pi * 2", 6.28318530718m
        },
        {
            "Euler", 2.71828182846m
        },
        {
            "((10 * 5 + -2 ^ 3 ^ 2) < -400) == 1 && !0 || 0", 1
        },

    };
}

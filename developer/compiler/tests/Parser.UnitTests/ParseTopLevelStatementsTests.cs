using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Execution;

using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;

namespace Parser.UnitTests;

public class ParseTopLevelStatementsTests
{
    private const int Precision = 5;

    private readonly Context context;
    private FakeEnvironment environment;


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

    [Fact]
    public void CanParseProgramWithInput()
    {
        List<decimal> inputValues = new List<decimal> { 50.5m };
        string code = @"
            float x = 10;
            print(x);
            input(x);
            print(x);
        ";
        environment = new FakeEnvironment(inputValues);
        Parser parser = new(context, environment, code);

        parser.ParseProgram();

        List<decimal> expected = new List<decimal> { 10, 50.5m };
        IReadOnlyList<decimal> actual = environment.Results;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanParseSmallProgram()
    {
        string code = @"
            float a;
            float b = 5;
            a = b + 3;
            print(a);
        ";
        Parser parser = new(context, environment, code);

        parser.ParseProgram();

        List<decimal> expected = new List<decimal> { 8 };
        IReadOnlyList<decimal> actual = environment.Results;
        Assert.Equal(expected, actual);
    }
}
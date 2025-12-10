using System;

using Execution;

using Parser;

namespace Interpreter.Specs;

public class InterpreterTests
{
    private const int Precision = 5;

    private readonly Interpreter interpreter;
    private readonly FakeEnvironment environment;

    public InterpreterTests()
    {
        environment = new FakeEnvironment();
        interpreter = new Interpreter(environment);
    }

    [Fact]
    public void CanExecuteSumNumbersProgram()
    {
        List<decimal> inputValues = new List<decimal> { 2.5m, 3.5m };
        environment.SetInput(inputValues);
        string code = @"
            float a;
            float b;
            input(a);
            input(b);
            float sum = a + b;
            print(sum);
        ";
        interpreter.Execute(code);

        List<decimal> expected = new List<decimal> { 6 };
        IReadOnlyList<decimal> actual = environment.Results;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CanExecuteCircleSquareProgram()
    {
        List<decimal> inputValues = new List<decimal> { 3 };
        environment.SetInput(inputValues);
        string code = @"
            float radius;
            input(radius);
            float area = Pi * radius ^ 2;
            print(area);
        ";

        interpreter.Execute(code);
        decimal expected = 28.27433m;
        IReadOnlyList<decimal> actual = environment.Results;
        Assert.Equal(expected, actual[0], Precision);
    }

    [Fact]
    public void CanExecuteFahrenheitToCelsiusProgram()
    {
        List<decimal> inputValues = new List<decimal> { 50 };
        environment.SetInput(inputValues);
        string code = @"
            float fahrenheit;
            input(fahrenheit);
            float celsius = (fahrenheit - 32.0) * 5.0 / 9.0;
            print(celsius);
        ";

        interpreter.Execute(code);
        decimal expected = 10;
        IReadOnlyList<decimal> actual = environment.Results;
        Assert.Equal(expected, actual[0], Precision);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser;

public class FakeEnvironment : Execution.IEnvironment
{
    private readonly List<decimal> output = [];
    private readonly List<decimal> input = [];

    public FakeEnvironment(List<decimal> input = null)
    {
        this.input = input ?? new List<decimal>();
    }

    public IReadOnlyList<decimal> Results => output;

    public void WriteNumber(decimal result)
    {
        output.Add(result);
    }

    public decimal ReadNumber()
    {
        decimal firstElement = input[0];
        input.RemoveAt(0);
        return firstElement;
    }
}
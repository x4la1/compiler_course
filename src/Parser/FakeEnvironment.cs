using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser;

public class FakeEnvironment : Execution.IEnvironment
{
    private readonly List<decimal> output = [];
    private Queue<decimal> input = [];

    public FakeEnvironment(IEnumerable<decimal>? inputValues = null)
    {
        if (inputValues != null)
        {
            SetInput(inputValues);
        }
    }

    public IReadOnlyList<decimal> Results => output;

    public void SetInput(IEnumerable<decimal> inputValues)
    {
        input = new Queue<decimal>(inputValues);
    }

    public void WriteNumber(decimal result)
    {
        output.Add(result);
    }

    public decimal ReadNumber()
    {
        return input.Dequeue();
    }
}
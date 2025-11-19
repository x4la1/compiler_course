using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser;

public class FakeEnvironment : Execution.IEnvironment
{
    private readonly List<decimal> output = [];
    private readonly Queue<decimal> input = [];

    public IReadOnlyList<decimal> Results => output;

    public FakeEnvironment(IEnumerable<decimal> inputValues = null)
    {
        this.input = new Queue<decimal>(inputValues ?? Enumerable.Empty<decimal>());
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
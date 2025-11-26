using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Execution;

public class ConsoleEnviroment : IEnvironment
{
    public decimal ReadNumber()
    {
        string? input = Console.ReadLine();

        if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }
        else
        {
            throw new InvalidInputException();
        }
    }

    public void WriteNumber(decimal result)
    {
        Console.WriteLine("Result: " + result.ToString("0.#####", CultureInfo.InvariantCulture));
    }
}
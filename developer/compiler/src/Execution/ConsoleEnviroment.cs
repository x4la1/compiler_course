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
        while (true)
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.Error.WriteLine("Invalid number");
                continue;
            }

            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
            else
            {
                Console.Error.WriteLine("Invalid number;");
            }
        }
    }

    public void WriteNumber(decimal result)
    {
        Console.WriteLine("Result: " + result.ToString("0.#####", CultureInfo.InvariantCulture));
    }
}
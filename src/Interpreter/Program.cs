using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Execution;

using Parser;

namespace Interpreter;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: Interpreter <file-path>");
            return 1;
        }

        string filePath = args[0];

        try
        {
            // Проверяем существование файла
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Error: File '{filePath}' not found.");
                return 1;
            }

            // Читаем исходный код целиком
            string sourceCode = File.ReadAllText(filePath);

            // Выполняем программу
            IEnvironment environment = new ConsoleEnviroment();
            Interpreter interpreter = new Interpreter(environment);
            interpreter.Execute(sourceCode);

            return 0;
        }
        catch (UnexpectedLexemeException ex)
        {
            Console.Error.WriteLine($"Parse error: {ex.Message}");
            return 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }
}
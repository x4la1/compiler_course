using System;

using Execution;

using Parser;

namespace Interpreter;

public class Interpreter
{
    private readonly Context context;
    private readonly IEnvironment environment;

    public Interpreter()
    {
        context = new Context();
        environment = new ConsoleEnviroment();
    }

    /// <summary>
    /// Выполняет программу на языке Kaleidoscope
    /// </summary>
    /// <param name="sourceCode">Исходный код программы</param>
    public void Execute(string sourceCode)
    {
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentException("Source code cannot be null or empty", nameof(sourceCode));
        }

        // Создаем парсер и выполняем программу
        Parser.Parser parser = new(context, environment, sourceCode);
        parser.ParseProgram();
    }
}

using System.Diagnostics.CodeAnalysis;

namespace Calculator.Infrastructure;

[ExcludeFromCodeCoverage]
public class ConsoleOutputProvider : IOutputProvider
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}
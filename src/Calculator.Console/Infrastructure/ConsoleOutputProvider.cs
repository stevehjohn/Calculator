using System.Diagnostics.CodeAnalysis;
using Calculator.Infrastructure;

namespace Calculator.Console.Infrastructure;

[ExcludeFromCodeCoverage]
public class ConsoleOutputProvider : IOutputProvider
{
    public void WriteLine(string line)
    {
        System.Console.WriteLine(line);
    }
}
using System.Diagnostics.CodeAnalysis;
using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Console.Infrastructure;

[ExcludeFromCodeCoverage]
public class ConsoleOutputProvider : IOutputProvider
{
    public void WriteLine(string line)
    {
        System.Console.WriteLine(line);
    }
}
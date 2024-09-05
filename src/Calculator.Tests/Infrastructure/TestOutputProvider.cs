using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Tests.Infrastructure;

public class TestOutputProvider : IOutputProvider
{
    private readonly List<string> _output = [];

    public IReadOnlyList<string> Output => _output;

    public TestOutputProvider()
    {
        Console.WriteLine();
    }
    
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
        
        _output.Add(line);
    }
}
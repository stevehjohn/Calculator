using Calculator.Infrastructure;

namespace Calculator.Tests.Infrastructure;

public class TestOutputProvider : IOutputProvider
{
    private readonly List<string> _output = new();

    public IReadOnlyList<string> Output => _output;
    
    public void WriteLine(string line)
    {
        _output.Add(line);
    }
}
namespace Calculator.Infrastructure;

public class ConsoleOutputProvider : IOutputProvider
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}
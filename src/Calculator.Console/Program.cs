using Calculator.Core;
using Calculator.Infrastructure;

namespace Calculator.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var evaluator = new Evaluator(new EvaluationLogger(new ConsoleOutputProvider()));

        System.Console.WriteLine();
        
        evaluator.Evaluate(args[0]);

        System.Console.WriteLine();
    }
}
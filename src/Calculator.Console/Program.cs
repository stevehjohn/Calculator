using System.Diagnostics.CodeAnalysis;
using Calculator.Console.Infrastructure;
using Calculator.Core;
using Calculator.Infrastructure;
using static System.Console;

namespace Calculator.Console;

[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly Evaluator Evaluator = new(new EvaluationLogger(new ConsoleOutputProvider()));

    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Interactive();
            
            return;
        }

        WriteLine();
        
        Evaluator.Evaluate(args[0]);

        WriteLine();
    }

    private static void Interactive()
    {
        WriteLine();

        while (true)
        {
            Write("> ");

            var input = ReadLine() ?? string.Empty;

            if (input.ToLower() is "quit" or "exit")
            {
                WriteLine();
                
                return;
            }
            
            WriteLine();

            try
            {
                Evaluator.Evaluate(input);
            }
            catch
            {
                WriteLine();
                
                WriteLine("Could not process the expression.");
            }


            WriteLine();
        }
    }
}
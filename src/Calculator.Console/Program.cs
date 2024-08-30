﻿using Calculator.Core;
using Calculator.Infrastructure;
using static System.Console;

namespace Calculator.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            WriteLine();
            
            WriteLine("Usage:");
            
            WriteLine();
            
            WriteLine("  Calculator.Console \"1 + 2 * (3 + 4)\"");
            
            WriteLine();
            
            WriteLine("  Provide the mathematical expression in quotes.");
            
            WriteLine();
        }

        var evaluator = new Evaluator(new EvaluationLogger(new ConsoleOutputProvider()));

        WriteLine();
        
        evaluator.Evaluate(args[0]);

        WriteLine();
    }
}
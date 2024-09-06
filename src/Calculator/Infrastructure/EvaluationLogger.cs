using System.Globalization;
using System.Text;
using Calculator.Extensions;
using Calculator.Interfaces;

namespace Calculator.Infrastructure;

public class EvaluationLogger
{
    private string _expression;

    private readonly IOutputProvider _output;

    public EvaluationLogger(IOutputProvider output)
    {
        _output = output;
    }

    public void SetExpression(string expression)
    {
        expression = expression.ToLower().Trim();

        // ReSharper disable StringIndexOfIsCultureSpecific.1
        while (expression.IndexOf(' ') > -1)
        {
            expression = expression.Replace(" ", string.Empty);
        }

        var builder = new StringBuilder();

        for (var i = 0; i < expression.Length - 1; i++)
        {
            if (expression[i] == ',')
            {
                builder.Append(", ");
                
                continue;
            }

            if (IsOperator(expression[i].ToString()))
            {
                builder.Append($" {expression[i]} ");
                
                continue;
            }

            if (IsOperator(expression[i..(i + 2)]))
            {
                builder.Append($" {expression[i..(i + 2)]} ");

                i++;
                
                continue;
            }

            builder.Append(expression[i]);
        }

        builder.Append(expression[^1]);

        _expression = builder.ToString();
        
        _output.WriteLine(_expression);
    }
    
    public void StepComplete(string operation, double result)
    {
        var expression = _expression.ReplaceLastOccurrence($"({operation})", result.ToString(CultureInfo.InvariantCulture));

        if (expression == _expression)
        {
            expression = _expression.ReplaceLastOccurrence(operation, result.ToString(CultureInfo.InvariantCulture));
        }

        _expression = expression;
        
        _output.WriteLine(_expression);
    }

    private static bool IsOperator(string characters)
    {
        return characters switch
        {
            "!" => true,
            "-" => true,
            "^" => true,
            "*" => true,
            "/" => true,
            "%" => true,
            "+" => true,
            "<<" => true,
            ">>" => true,
            _ => false
        };
    }
}
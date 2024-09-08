using System.Globalization;
using System.Text;
using Calculator.Extensions;
using Calculator.Interfaces;

namespace Calculator.Infrastructure;

public class EvaluationLogger
{
    private string _expression;

    private readonly IOutputProvider _output;

    private string _previous;

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

        var isUnary = true;
        
        for (var i = 0; i < expression.Length - 1; i++)
        {
            if (expression[i] == ',')
            {
                builder.Append(", ");

                isUnary = true;
                
                continue;
            }

            if (IsBinaryOperator(expression[i].ToString()))
            {
                if (isUnary)
                {
                    builder.Append($" {expression[i]}");
                }
                else
                {
                    builder.Append($" {expression[i]} ");
                
                    isUnary = true;
                }

                continue;
            }
            
            if (IsUnaryOperator(expression[i].ToString()))
            {
                builder.Append(expression[i]);
                
                continue;
            }

            if (IsBinaryOperator(expression[i..(i + 2)]))
            {
                builder.Append($" {expression[i..(i + 2)]} ");

                isUnary = true;

                i++;
                
                continue;
            }

            builder.Append(expression[i]);

            isUnary = false;
        }

        builder.Append(expression[^1]);

        expression = builder.ToString();

        _expression = expression;
        
        while (expression.IndexOf("  ") > -1)
        {
            expression = expression.Replace("  ", " ");
        }

        _previous = expression;
        
        _output.WriteLine(expression);
    }
    
    public void StepComplete(string operation, double result)
    {
        var expression = _expression.ReplaceLastOccurrence($"({operation})", result.ToString(CultureInfo.InvariantCulture));

        if (expression == _expression)
        {
            expression = _expression.ReplaceLastOccurrence(operation, result.ToString(CultureInfo.InvariantCulture));
        }

        _expression = expression;

        while (expression.IndexOf("  ") > -1)
        {
            expression = expression.Replace("  ", " ");
        }

        if (expression == _previous)
        {
            return;
        }

        _output.WriteLine(expression);

        _previous = expression;
    }

    private static bool IsBinaryOperator(string characters)
    {
        return characters switch
        {
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
    
    private static bool IsUnaryOperator(string characters)
    {
        return characters switch
        {
            "!" => true,
            _ => false
        };
    }
}
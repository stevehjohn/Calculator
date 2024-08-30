using System.Globalization;
using Calculator.Extensions;

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
        expression = expression.ToLower();

        // ReSharper disable StringIndexOfIsCultureSpecific.1
        while (expression.IndexOf("  ") > -1)
        {
            expression = expression.Replace("  ", " ");
        }

        while (expression.IndexOf("( ") > -1)
        {
            expression = expression.Replace("( ", "(");
        }

        while (expression.IndexOf(" )") > -1)
        {
            expression = expression.Replace(" )", ")");
        }

        _expression = expression;
        
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
}
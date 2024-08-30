using Calculator.Extensions;

namespace Calculator.Infrastructure;

public class EvaluationLogger
{
    private string _expression;

    private readonly ConsoleOutputProvider _output;

    public EvaluationLogger(ConsoleOutputProvider output)
    {
        _output = output;
    }

    public void SetExpression(string expression)
    {
        expression = expression.ToLower();

        // ReSharper disable once StringIndexOfIsCultureSpecific.1
        while (expression.IndexOf("  ") > -1)
        {
            expression = expression.Replace("  ", " ");
        }

        _expression = expression;
        
        _output.WriteLine(_expression);
    }
    

    public void StepComplete(string operation)
    {
        var expression = _expression.ReplaceLastOccurrence($"({operation})", string.Empty);

        if (expression == _expression)
        {
            expression = _expression.ReplaceLastOccurrence(operation, string.Empty);
        }

        _expression = expression;
        
        _output.WriteLine(_expression);
    }
}
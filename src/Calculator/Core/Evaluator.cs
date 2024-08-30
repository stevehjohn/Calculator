using Calculator.Infrastructure;

namespace Calculator.Core;

public class Evaluator
{
    private readonly Parser _parser = new();

    private readonly Stack<Element> _stack = new();

    private readonly EvaluationLogger _logger;

    public Evaluator()
    {
    }
    
    public Evaluator(EvaluationLogger logger)
    {
        _logger = logger;
    }
    
    public double Evaluate(string expression)
    {
        _logger?.SetExpression(expression);
        
        var queue = _parser.Parse(expression);
        
        _stack.Clear();
        
        foreach (var element in queue)
        {
            element.Process(_stack);
        }

        var result = _stack.Pop().Value;
        
        return result;
    }
}
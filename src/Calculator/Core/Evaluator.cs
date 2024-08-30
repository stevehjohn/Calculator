using Calculator.Infrastructure;

namespace Calculator.Core;

public class Evaluator
{
    private readonly Parser _parser = new();

    private readonly Stack<Element> _stack = new();

    private readonly EvaluationLogger _logger;

    public List<string> Steps => _logger?.GetSteps();
    
    public Evaluator(bool log = false)
    {
        if (log)
        {
            _logger = new EvaluationLogger();
        }
    }
    
    public double Evaluate(string expression)
    {
        var queue = _parser.Parse(expression);
        
        _stack.Clear();
        
        _logger?.SetUp(expression, _stack);
        
        foreach (var element in queue)
        {
            element.Process(_stack);
            
            _logger?.StepComplete();
        }

        var result = _stack.Pop().Value;
        
        return result;
    }
}
namespace Calculator.Core;

public class Evaluator
{
    private readonly Parser _parser = new();

    private readonly Stack<Element> _stack = new();
    
    public double Evaluate(string expression)
    {
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
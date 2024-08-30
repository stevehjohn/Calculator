using Calculator.Core;

namespace Calculator.Infrastructure;

public class EvaluationLogger
{
    private string _expression;

    private Stack<Element> _stack;

    private readonly List<string> _steps = new();

    public void SetUp(string expression, Stack<Element> stack)
    {
        _expression = expression;

        _stack = stack;
        
        _steps.Clear();
    }

    public void StepComplete()
    {
    }

    public List<string> GetSteps()
    {
        return _steps;
    }
}
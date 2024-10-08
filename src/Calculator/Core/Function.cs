using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Core;

public class Function : Element, ICreatableElement<Function>
{
    private readonly string _function;

    private Function(string function)
    {
        _function = function.ToLower();
    }

    public static Function CreateInstance(string expression)
    {
        return expression.ToLower() switch
        {
            "max" => new Function("max"),
            "sin" => new Function("sin"),
            "sqrt" => new Function("sqrt"),
            _ => null
        };
    }

    public override void Process(Stack<Element> stack, EvaluationLogger logger = null)
    {
        double value;
        
        switch (_function)
        {
            case "max":
                var left = stack.Pop().Value;
                
                var right = stack.Pop().Value;
                
                stack.Push(Create(Math.Max(left, right)));

                logger?.StepComplete($"max({right}, {left})", stack.Peek().Value);
                
                break;

            case "sin":
                value = stack.Pop().Value;

                stack.Push(Create(Math.Sin(value)));

                logger?.StepComplete($"sin({value})", stack.Peek().Value);

                break;

            case "sqrt":
                value = stack.Pop().Value;
                
                stack.Push(Create(Math.Sqrt(value)));
                
                logger?.StepComplete($"sqrt({value})", stack.Peek().Value);
                
                break;
        }
    }

    public override string ToString()
    {
        return _function;
    }
}
using Calculator.Exceptions;
using Calculator.Infrastructure;

namespace Calculator.Core;

public class Function : Element
{
    private readonly string _function;

    public Function(string function)
    {
        _function = function.ToLower();
    }

    public override void Process(Stack<Element> stack, EvaluationLogger logger = null)
    {
        double value;
        
        switch (_function)
        {
            case "max":
                var left = stack.Pop().Value;
                
                var right = stack.Pop().Value;
                
                stack.Push(new Operand(Math.Max(left, right)));

                logger?.StepComplete($"max({right}, {left})", stack.Peek().Value);
                
                break;

            case "sin":
                value = stack.Pop().Value;

                stack.Push(new Operand(Math.Sin(value)));

                logger?.StepComplete($"sin({value})", stack.Peek().Value);

                break;

            case "sqrt":
                value = stack.Pop().Value;
                
                stack.Push(new Operand(Math.Sqrt(value)));
                
                logger?.StepComplete($"sqrt({value})", stack.Peek().Value);
                
                break;
            
            default:
                throw new ParseException($"Unknown function {_function}.");
        }
    }

    public override string ToString()
    {
        return _function;
    }
}
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
        switch (_function)
        {
            case "max":
                stack.Push(new Operand(Math.Max(stack.Pop().Value, stack.Pop().Value)));

                break;

            case "sin":
                stack.Push(new Operand(Math.Sin(stack.Pop().Value)));

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
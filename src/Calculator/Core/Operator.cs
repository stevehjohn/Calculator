using Calculator.Exceptions;

namespace Calculator.Core;

public class Operator : Element
{
    private readonly Operation _operation;
    
    public Operator(char operation)
    {
        _operation = operation switch
        {
            '+' => Operation.Add,
            '/' => Operation.Divide,
            '*' => Operation.Multiply,
            '-' => Operation.Subtract,
            _ => throw new ParseException($"Unknown operator type '{operation}'.")
        };
    }

    public override void Process(Stack<Element> stack)
    {
        var right = stack.Pop().Value;

        var left = stack.Pop().Value;

        stack.Push(new Operand(_operation switch
        {
            Operation.Add => left + right,
            Operation.Subtract => left - right,
            Operation.Multiply => left * right,
            _ => left / right
        }));
    }

    public override string ToString()
    {
        return _operation.ToString();
    }
}
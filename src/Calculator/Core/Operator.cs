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
            '^' => Operation.Exponentiate,
            '*' => Operation.Multiply,
            '_' => Operation.Negate,
            '-' => Operation.Subtract,
            _ => throw new ParseException($"Unknown operator type '{operation}'.")
        };
    }

    public override void Process(Stack<Element> stack)
    {
        if (_operation == Operation.Negate)
        {
            stack.Push(new Operand(-stack.Pop().Value));
            
            return;
        }

        var right = stack.Pop().Value;

        var left = stack.Pop().Value;

        stack.Push(new Operand(_operation switch
        {
            Operation.Add => left + right,
            Operation.Divide => left / right,
            Operation.Exponentiate => Math.Pow(left, right),
            Operation.Subtract => left - right,
            Operation.Multiply => left * right,
            _ => throw new ParseException($"Unable to perform {_operation}.")
        }));
    }

    public override string ToString()
    {
        return _operation.ToString();
    }
}
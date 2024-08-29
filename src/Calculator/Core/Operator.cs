using Calculator.Exceptions;
using Calculator.Libraries;

namespace Calculator.Core;

public class Operator : Element
{
    private readonly Operation _operation;
    
    public Operator(string operation)
    {
        _operation = operation switch
        {
            "+" => Operation.Add,
            "/" => Operation.Divide,
            "^" => Operation.Exponentiate,
            "!" => Operation.Factorial,
            "<<" => Operation.LeftShift,
            "%" => Operation.Modulus,
            "*" => Operation.Multiply,
            "--" => Operation.Negate,
            ">>" => Operation.RightShift,
            "-" => Operation.Subtract,
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

        if (_operation == Operation.Factorial)
        {
            stack.Push(new Operand(Maths.Factorial((long) stack.Pop().Value)));
        }

        var right = stack.Pop().Value;

        var left = stack.Pop().Value;

        stack.Push(new Operand(_operation switch
        {
            Operation.Add => left + right,
            Operation.Divide => left / right,
            Operation.Exponentiate => Math.Pow(left, right),
            Operation.LeftShift => (long) left << (int) right,
            Operation.Modulus => left % right,
            Operation.Multiply => left * right,
            Operation.RightShift => (long) left >> (int) right,
            Operation.Subtract => left - right,
            _ => throw new ParseException($"Unable to perform {_operation}.")
        }));
    }

    public override string ToString()
    {
        return _operation.ToString();
    }
}
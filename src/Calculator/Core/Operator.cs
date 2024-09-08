#pragma warning disable CS8509

using Calculator.Infrastructure;
using Calculator.Interfaces;
using Calculator.Libraries;

namespace Calculator.Core;

public class Operator : Element, ICreatableElement<Operator>
{
    private readonly Operation _operation;
    
    private Operator(Operation operation)
    {
        _operation = operation;
    }

    public static Operator CreateInstance(string expression)
    {
        return expression switch
        {
            "+" => new Operator(Operation.Add),
            "/" => new Operator(Operation.Divide),
            "^" => new Operator(Operation.Exponentiate),
            "!" => new Operator(Operation.Factorial),
            "<<" => new Operator(Operation.LeftShift),
            "%" => new Operator(Operation.Modulus),
            "*" => new Operator(Operation.Multiply),
            "--" => new Operator(Operation.Negate),
            ">>" => new Operator(Operation.RightShift),
            "-" => new Operator(Operation.Subtract),
            _ => null
        };
    }
    
    public override void Process(Stack<Element> stack, EvaluationLogger logger = null)
    {
        double value;
        
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (_operation)
        {
            case Operation.Negate:
                value = stack.Pop().Value;
                
                stack.Push(Create(-value));

                logger?.StepComplete($" -{value}", stack.Peek().Value);

                return;
            
            case Operation.Factorial:
                value = stack.Pop().Value;
                
                stack.Push(Create(Maths.Factorial((long) value)));
                
                logger?.StepComplete($"{value}!", stack.Peek().Value);
            
                return;
        }

        var right = stack.Pop().Value;

        var left = stack.Pop().Value;

        stack.Push(Create(_operation switch
        {
            Operation.Add => left + right,
            Operation.Divide => left / right,
            Operation.Exponentiate => Math.Pow(left, right),
            Operation.LeftShift => (long) left << (int) right,
            Operation.Modulus => left % right,
            Operation.Multiply => left * right,
            Operation.RightShift => (long) left >> (int) right,
            Operation.Subtract => left - right
        }));

        logger?.StepComplete(_operation switch
        {
            Operation.Add => $"{left} + {right}",
            Operation.Divide => $"{left} / {right}",
            Operation.Exponentiate => $"{left} ^ {right}",
            Operation.LeftShift => $"{(long) left} << {(int) right}",
            Operation.Modulus => $"{left} % {right}",
            Operation.Multiply => $"{left} * {right}",
            Operation.RightShift => $"{(long) left} >> {(int) right}",
            Operation.Subtract => $"{left} - {right}"
        }, stack.Peek().Value);
    }

    public override string ToString()
    {
        return _operation.ToString();
    }
}
#pragma warning disable CS8509

using Calculator.Exceptions;
using Calculator.Infrastructure;
using Calculator.Libraries;

namespace Calculator.Core;

public class Operator : Element
{
    private readonly Operation _operation;

    private EvaluationLogger _logger;
    
    public Operator(string operation, EvaluationLogger logger = null)
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

        _logger = logger;
    }

    public override void Process(Stack<Element> stack)
    {
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (_operation)
        {
            case Operation.Negate:
                stack.Push(new Operand(-stack.Pop().Value));
            
                return;
            case Operation.Factorial:
                var value = stack.Pop().Value;
                
                stack.Push(new Operand(Maths.Factorial((long) value)));
                
                _logger.StepComplete($"{value}");
            
                return;
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
            Operation.Subtract => left - right
        }));

        _logger?.StepComplete(_operation switch
        {
            Operation.Add => $"{left} + {right}",
            Operation.Divide => $"{left} / {right}",
            Operation.Exponentiate => $"{left} ^ {right}",
            Operation.LeftShift => $"{(long) left} << {(long) right}",
            Operation.Modulus => $"{left} % {right}",
            Operation.Multiply => $"{left} * {right}",
            Operation.RightShift => $"{(long) left} >> {(long) right}",
            Operation.Subtract => $"{left} - {right}"
        });
    }

    public override string ToString()
    {
        return _operation.ToString();
    }
}
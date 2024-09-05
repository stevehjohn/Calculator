using System.Globalization;
using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Core;

public class Operand : Element, ICreatableElement
{
    public override double Value { get; }

    private Operand(double value)
    {
        Value = value;
    }

    public static Element CreateInstance(string expression)
    {
        if (double.TryParse(expression, out var value))
        {
            return new Operand(value);
        }

        return null;
    }

    public override void Process(Stack<Element> stack, EvaluationLogger logger = null)
    {
        stack.Push(this);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
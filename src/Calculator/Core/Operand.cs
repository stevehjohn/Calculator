using System.Globalization;

namespace Calculator.Core;

public class Operand : Element
{
    public override double Value { get; }

    public Operand(double value)
    {
        Value = value;
    }

    public override void Process(Stack<Element> stack)
    {
        stack.Push(this);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
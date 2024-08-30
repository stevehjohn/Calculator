using Calculator.Exceptions;
using Calculator.Infrastructure;

namespace Calculator.Core;

public abstract class Element
{
    public abstract void Process(Stack<Element> stack, EvaluationLogger logger = null);
        
    public virtual double Value => throw new ParseException($"Incorrect call to .Value on Element type {GetType().Name}.");
    
    public static Element Create(string symbol)
    {
        if (char.IsLetter(symbol[0]))
        {
            return new Function(symbol);
        }

        return new Operator(symbol);
    }

    public static Element Create(double value)
    {
        return new Operand(value);
    }
}
using Calculator.Exceptions;

namespace Calculator.Core;

public abstract class Element
{
    public abstract void Process(Stack<Element> stack);
        
    public virtual double Value => throw new ParseException($"Incorrect call to .Value on Element type {GetType().Name}.");
}
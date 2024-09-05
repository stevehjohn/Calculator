using Calculator.Exceptions;
using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Core;

public abstract class Element
{
    public abstract void Process(Stack<Element> stack, EvaluationLogger logger = null);
        
    public virtual double Value => throw new ParseException($"Incorrect call to .Value on Element type {GetType().Name}.");
    
    public static Element Create(string expression)
    {
        Element instance = Operator.CreateInstance(expression);

        instance ??= Function.CreateInstance(expression);

        instance ??= Operand.CreateInstance(expression);

        if (instance == null)
        {
            throw new ParseException($"Unable to parse expression '{expression}'.");
        }

        return instance;
    }
}
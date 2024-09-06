using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Core;

public class Constant : Element, ICreatableElement<Constant>
{
    private readonly string _name;

    private readonly double _value;

    private Constant(string name, double value)
    {
        _name = name;
        
        _value = value;
    }

    public static Constant CreateInstance(string expression)
    {
        return expression.ToLower() switch
        {
            "π" => new Constant(expression, Math.PI),
            _ => null
        };
    }

    public override void Process(Stack<Element> stack, EvaluationLogger logger = null)
    {
        stack.Push(Create( _value));
        
        logger?.StepComplete(_name, _value);
    }
}
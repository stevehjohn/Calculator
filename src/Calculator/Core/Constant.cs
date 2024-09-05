using Calculator.Infrastructure;
using Calculator.Interfaces;

namespace Calculator.Core;

public class Constant : Element, ICreatableElement
{
    private string _name;

    private double _value;

    private Constant(string name, double value)
    {
        _name = name;

        _value = value;
    }

    public static Element CreateInstance(string expression)
    {
        return expression switch
        {
            "π" => new Constant("π", Math.PI),
            _ => null
        };
    }

    public override void Process(Stack<Element> stack, EvaluationLogger logger = null)
    {
        throw new NotImplementedException();
    }
}
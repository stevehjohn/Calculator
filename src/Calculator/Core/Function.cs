namespace Calculator.Core;

public class Function : Element
{
    private readonly string _function;
    
    public Function(string function)
    {
        _function = function.ToLower();
    }
    
    public override void Process(Stack<Element> stack)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return _function;
    }
}
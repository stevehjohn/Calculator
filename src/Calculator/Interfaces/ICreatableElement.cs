using Calculator.Core;

namespace Calculator.Interfaces;

public interface ICreatableElement
{
    static Element CreateInstance(string expression)
    {
        return null;
    }
}
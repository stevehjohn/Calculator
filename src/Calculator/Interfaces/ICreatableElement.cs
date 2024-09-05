using Calculator.Core;

namespace Calculator.Interfaces;

public interface ICreatableElement
{
    static abstract Element CreateInstance(string expression);
}
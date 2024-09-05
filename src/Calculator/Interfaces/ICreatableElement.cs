using Calculator.Core;

namespace Calculator.Interfaces;

public interface ICreatableElement<out T> where T : Element
{
    static abstract T CreateInstance(string expression);
}
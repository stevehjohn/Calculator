using Calculator.Core;

namespace Calculator.Interfaces;

public interface ICreatableElement<out T> where T : Element
{
    // ReSharper disable once UnusedMemberInSuper.Global - Enforces implementations
    static abstract T CreateInstance(string expression);
}
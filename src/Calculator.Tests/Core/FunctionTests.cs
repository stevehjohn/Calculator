using Calculator.Core;
using Calculator.Exceptions;
using Xunit;

namespace Calculator.Tests.Core;

public class FunctionTests
{
    [Fact]
    public void UnknownFunctionThrowsAnException()
    {
        var function = new Function("foo");

        Assert.Throws<ParseException>(() => function.Process(new Stack<Element>()));
    }

    [Fact]
    public void SqrtReturnsSquareRoot()
    {
        var function = Element.Create("sqrt");

        var stack = new Stack<Element>();
        
        stack.Push(Element.Create(16));
        
        function.Process(stack);
        
        Assert.Equal(4, stack.Pop().Value);
    }

    [Fact]
    public void MaxReturnsMaximum()
    {
        var function = Element.Create("max");

        var stack = new Stack<Element>();
        
        stack.Push(Element.Create(16));
        
        stack.Push(Element.Create(20));
        
        function.Process(stack);
        
        Assert.Equal(20, stack.Pop().Value);
    }
}
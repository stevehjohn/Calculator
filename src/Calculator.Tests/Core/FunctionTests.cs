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
}
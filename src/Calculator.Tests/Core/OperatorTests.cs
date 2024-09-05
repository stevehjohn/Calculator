using Calculator.Core;
using Calculator.Exceptions;
using Xunit;

namespace Calculator.Tests.Core;

public class OperatorTests
{
    [Fact]
    public void CallingValueOnOperatorThrowsException()
    {
        var element = Element.Create("+");

        Assert.Throws<ParseException>(() => element.Value);
    }

    [Fact]
    public void OperatorThrowsExceptionWhenPassedInvalidSymbol()
    {
        Assert.Throws<ParseException>(() => Element.Create("@"));
    }
}
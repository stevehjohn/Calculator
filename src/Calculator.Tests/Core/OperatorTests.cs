using Calculator.Core;
using Calculator.Exceptions;
using Xunit;

namespace Calculator.Tests.Core;

public class OperatorTests
{
    [Fact]
    public void CallingValueOnOperatorThrowsException()
    {
        var element = new Operator("+");

        Assert.Throws<ParseException>(() => element.Value);
    }

    [Fact]
    public void OperatorThrowsExceptionWhenPassedInvalidSymbol()
    {
        Assert.Throws<ParseException>(() => new Operator("@"));
    }
}
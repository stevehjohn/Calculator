using Calculator.Core;
using Xunit;

namespace Calculator.Tests.Core;

public class OperandTests
{
    [Fact]
    public void OutputsOperand()
    {
        var operand = Element.Create("123");
        
        Assert.Equal("123", operand.ToString());
    }
}
using Calculator.Core;
using Xunit;

namespace Calculator.Tests.Core;

public class OperandTests
{
    [Fact]
    public void OutputsOperand()
    {
        var operand = new Operand(123d);
        
        Assert.Equal("123", operand.ToString());
    }
}
using Calculator.Core;
using Xunit;

namespace Calculator.Tests.Core;

public class EvaluatorTests
{
    private readonly Evaluator _evaluator = new();
    
    [Theory]
    [InlineData("2 * 3", 6)]
    [InlineData("2.5 / .5", 5)]
    [InlineData("5 * -3", -15)]
    [InlineData("4 ^ 2", 16)]
    [InlineData("4 ^ -2", 0.0625)]
    [InlineData("(5 + 1) * (8 - 2)", 36)]
    [InlineData("5 + 1 * (8 - 2)", 11)]
    [InlineData("(1 + 2 + 3) * 4", 24)]
    [InlineData("1 << 2", 4)]
    [InlineData("8 >> 2", 2)]
    [InlineData("5 % 3", 2)]
    [InlineData("6 % 3", 0)]
    [InlineData("5 % 2.5", 0)]
    [InlineData("6 % 2.5", 1)]
    [InlineData("1 << 1 + 2", 8)]
    [InlineData("10 + 8 % 3", 12)]
    public void ProducesExpectedResult(string expression, double expected)
    {
        var result = _evaluator.Evaluate(expression);
        
        Assert.Equal(expected, result);
    }
}
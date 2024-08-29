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
    public void ProducesExpectedResult(string expression, double expected)
    {
        var result = _evaluator.Evaluate(expression);
        
        Assert.Equal(expected, result);
    }
}
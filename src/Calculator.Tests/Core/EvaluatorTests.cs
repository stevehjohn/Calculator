using Calculator.Core;
using Xunit;

namespace Calculator.Tests.Core;

public class EvaluatorTests
{
    private readonly Evaluator _evaluator = new();
    
    [Theory]
    [InlineData("2 * 3", 6)]
    [InlineData("2.5 / .5", 5)]
    public void ProducesExpectedResult(string expression, double expected)
    {
        var result = _evaluator.Evaluate(expression);
        
        Assert.Equal(expected, result);
    }
}
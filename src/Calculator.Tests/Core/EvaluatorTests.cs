using Calculator.Core;
using Calculator.Infrastructure;
using Calculator.Tests.Infrastructure;
using Xunit;

namespace Calculator.Tests.Core;

public class EvaluatorTests
{
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
    [InlineData("5! + 1", 121)]
    [InlineData("5! - 1", 119)]
    [InlineData("2 + max(3, 1)", 5)]
    [InlineData("sin(1)", 0.8414709848078965)]
    [InlineData("π + 1", 4.141592653589793)]
    public void ProducesExpectedResult(string expression, double expected)
    {
        var evaluator = new Evaluator();
        
        var result = evaluator.Evaluate(expression);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("(5 + 1) * (8 - 2)", "(5 + 1) * (8 - 2)|6 * (8 - 2)|6 * 6|36")]
    [InlineData("5 + 1 * (8 - 2)", "5 + 1 * (8 - 2)|5 + 1 * 6|5 + 6|11")]
    [InlineData("(1 + 2 + 3) * 4", "(1 + 2 + 3) * 4|(3 + 3) * 4|6 * 4|24")]
    [InlineData("sin(1) + 1", "sin(1) + 1|0.8414709848078965 + 1|1.8414709848078965")]
    [InlineData("2 + max(3, 1)", "2 + max(3, 1)|2 + 3|5")]
    public void OutputsOperationsWhenProvidedWithLogger(string expression, string expected)
    {
        var parts = expected.Split('|');

        var outputProvider = new TestOutputProvider();
        
        var evaluator = new Evaluator(new EvaluationLogger(outputProvider));
        
        evaluator.Evaluate(expression);

        for (var i = 0; i < parts.Length; i++)
        {
            Assert.Equal(parts[i], outputProvider.Output[i]);
        }
    }
}
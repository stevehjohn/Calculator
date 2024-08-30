using Calculator.Core;
using Xunit;

namespace Calculator.Tests.Infrastructure;

public class EvaluationLoggerTests
{
    private readonly Evaluator _evaluator = new(true);
    
    [Theory]
    [InlineData("(5 + 1) * (8 - 2)", "(5 + 1) * 6|6 * 6|36")]
    public void OutputsCorrectSteps(string expression, string expected)
    {
        var expectedSteps = expected.Split('|');

        _evaluator.Evaluate(expression);

        var steps = _evaluator.Steps;

        for (var i = 0; i < expectedSteps.Length; i++)
        {
            Assert.Equal(expectedSteps[i], steps[i]);
        }
    }
}
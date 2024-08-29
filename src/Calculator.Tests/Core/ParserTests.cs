using Calculator.Core;
using Xunit;

namespace Calculator.Tests.Core;

public class ParserTests
{
    private readonly Parser _parser = new();

    [Theory]
    [InlineData("1 + 5 * (8 - 2)", "1 5 8 2 - * +")]
    private void ParsesExpressionsCorrectly(string expression, string expected)
    {
        var result = _parser.Parse(expression);

        Assert.Equal(expected, string.Join(' ', result));
    }
}
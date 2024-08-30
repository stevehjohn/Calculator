#pragma warning disable CS8509

using System.Text;
using Calculator.Core;
using Calculator.Exceptions;
using Xunit;

namespace Calculator.Tests.Core;

public class ParserTests
{
    private readonly Parser _parser = new();

    [Theory]
    [InlineData("1 + 5 * (8 - 2)", "1 5 8 2 - * +")]
    [InlineData("3 + 4 * 2 / (1 - 5) ^ 2 ^ 3", "3 4 2 * 1 5 - 2 3 ^ ^ / +")]
    [InlineData("2.5 * 2", "2.5 2 *")]
    [InlineData("1 << 1 + 2", "1 1 2 + <<")]
    [InlineData("1 >> 1 + 2", "1 1 2 + >>")]
    [InlineData("2 + -1", "2 1 -- +")]
    [InlineData("2 + 5 % 2", "2 5 2 % +")]
    [InlineData("5! + 1", "5 ! 1 +")]
    [InlineData("5! - 1", "5 ! 1 -")]
    [InlineData("sin(max(2, 3) / 3 * 1)", "2 3 max 3 / 1 * sin")]
    [InlineData("π + 1", "3.141592653589793 1 +")]
    public void ParsesExpressionsCorrectly(string expression, string expected)
    {
        var result = _parser.Parse(expression);

        Assert.Equal(expected, ConstructExpected(result));
    }

    [Fact]
    public void ParserThrowsExceptionForUnknownOperator()
    {
        Assert.Throws<ParseException>(() => _parser.Parse("1 @ 2"));
    }

    private static string ConstructExpected(Queue<Element> queue)
    {
        var expected = new StringBuilder();
        
        while (queue.TryDequeue(out var item))
        {
            switch (item)
            {
                case Operator operation:
                    expected.Append(operation.ToString() switch
                    {
                        "Add" => "+ ",
                        "Divide" => "/ ",
                        "Exponentiate" => "^ ",
                        "Factorial" => "! ",
                        "LeftShift" => "<< ",
                        "Modulus" => "% ",
                        "Multiply" => "* ",
                        "Negate" => "-- ",
                        "RightShift" => ">> ",
                        "Subtract" => "- "
                    });
                
                    continue;
                case Function:
                    expected.Append($"{item.ToString()} ");
                
                    continue;
                default:
                    expected.Append($"{item.Value} ");
                    
                    break;
            }
        }

        return expected.ToString().Trim();
    }
}
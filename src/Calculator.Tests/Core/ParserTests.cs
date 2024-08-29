using System.Text;
using Calculator.Core;
using Calculator.Exceptions;
using Calculator.Tests.Exceptions;
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
        
        while (queue.TryDequeue(out Element item))
        {
            if (item is Operator operation)
            {
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
                    "Subtract" => "- ",
                    _ => throw new TestException($"Unrecognised operation {operation}.")
                });
                
                continue;
            }

            expected.Append($"{item.Value} ");
        }

        return expected.ToString().Trim();
    }
}
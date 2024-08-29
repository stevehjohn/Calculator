using System.Text;
using Calculator.Core;
using Calculator.Tests.Exceptions;
using Xunit;

namespace Calculator.Tests.Core;

public class ParserTests
{
    private readonly Parser _parser = new();

    [Theory]
    [InlineData("1 + 5 * (8 - 2)", "1 5 8 2 - * +")]
    [InlineData("3 + 4 * 2 / (1 - 5) ^ 2 ^ 3", "3 4 2 * 1 5 - 2 3 ^ ^ / +")]
    public void ParsesExpressionsCorrectly(string expression, string expected)
    {
        var result = _parser.Parse(expression);

        Assert.Equal(expected, ConstructExpected(result));
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
                    "Multiply" => "* ",
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
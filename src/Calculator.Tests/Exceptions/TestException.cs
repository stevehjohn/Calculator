using System.Diagnostics.CodeAnalysis;

namespace Calculator.Tests.Exceptions;

[ExcludeFromCodeCoverage]
public class TestException : Exception
{
    public TestException(string message) : base(message)
    {
    }
}
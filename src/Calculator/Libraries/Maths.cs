namespace Calculator.Libraries;

public static class Maths
{
    public static long Factorial(long value)
    {
        var result = 1L;

        while (value > 1)
        {
            result *= value;

            value--;
        }

        return result;
    }
}
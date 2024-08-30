namespace Calculator.Extensions;

public static class StringExtensions
{
    public static string ReplaceLastOccurrence(this string source, string target, string replacement)
    {
        // ReSharper disable once StringLastIndexOfIsCultureSpecific.1
        var position = source.LastIndexOf(target);

        if (position == -1)
        {
            return source;
        }

        return source.Remove(position, target.Length).Insert(position, replacement);
    }
}
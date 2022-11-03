namespace Lolcat;

internal static class Extensions
{
    // ReSharper disable once RedundantNullableFlowAttribute
    public static double ThrowIfOutOfRange([NotNull] this double argument, double min, double max,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument >= min && argument <= max)
        {
            return argument;
        }

        var message = argument < min
            ? $"Less than {nameof(min)} '{min}'"
            : $"Greater than {nameof(max)} '{max}'";

        throw new ArgumentOutOfRangeException(paramName, argument, message);
    }

    // ReSharper disable once RedundantNullableFlowAttribute
    public static double ThrowIfZeroOrLess([NotNull] this double argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument > 0)
        {
            return argument;
        }

        throw new ArgumentOutOfRangeException(paramName, argument, "Zero or less");
    }

}

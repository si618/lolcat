using System.Globalization;

namespace Lolcat;

internal static class Extensions
{
    extension(double argument)
    {
        public double ThrowIfOutOfRange(double min, double max,
            [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument >= min && argument <= max)
            {
                return argument;
            }

            string message = argument < min
                ? string.Create(CultureInfo.InvariantCulture, $"Less than {nameof(min)} '{min}'")
                : string.Create(CultureInfo.InvariantCulture, $"Greater than {nameof(max)} '{max}'");

            throw new ArgumentOutOfRangeException(paramName, argument, message);
        }
    }

    extension(int argument)
    {
        public int ThrowIfLessThanOne(
            [CallerArgumentExpression(nameof(argument))] string? paramName = null) =>
            argument >= 1
                ? argument
                : throw new ArgumentOutOfRangeException(paramName, argument, "Less than 1");
    }
}

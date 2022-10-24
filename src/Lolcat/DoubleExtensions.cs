namespace Lolcat;

public static class DoubleExtensions
{
	// ReSharper disable once RedundantNullableFlowAttribute
	public static double ThrowIfOutOfRange([NotNull] this double argument, double min, double max,
		[CallerArgumentExpression("argument")] string? paramName = null)
	{
		if (argument < min || argument > max)
		{
			throw new ArgumentOutOfRangeException(paramName,
			argument,
			argument < min ? $"Less than min '{min}'" : $"Greater than '{max}'");
		}

		return argument;
	}
}

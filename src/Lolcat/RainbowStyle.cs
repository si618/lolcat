namespace Lolcat;

public record RainbowStyle(
	EscapeSequence EscapeSequence = EscapeSequence.Ansi,
	double Spread = 3,
	double Frequency = .1,
	int? Seed = null)
{
	public EscapeSequence EscapeSequence { get; set; } = EscapeSequence;
	public double Spread { get; set; } = Spread.ThrowIfOutOfRange(1, 10);
	public double Frequency { get; set; } = Frequency.ThrowIfOutOfRange(0.001, 1);
	public int? Seed { get; set;  } = Seed;
}

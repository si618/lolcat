namespace Lolcat;

/// <summary>Rainbow style options</summary>
/// <param name="EscapeSequence">Escape sequence used to generate colours (default: Ansi)</param>
/// <param name="Spread">Rainbow spread (default: 3.0)</param>
/// <param name="Frequency">Rainbow frequency (default: 0.1)</param>
/// <param name="Seed">Random seed, 0 = random (default: 0)</param>
public sealed record RainbowStyle(
    EscapeSequence EscapeSequence = EscapeSequence.Ansi,
    double Spread = 3,
    double Frequency = .1,
    double Seed = 0)
{
    /// <summary>
    /// Escape sequence used to generate colours (default: Ansi)
    /// </summary>
    public EscapeSequence EscapeSequence { get; init; } = EscapeSequence;

    /// <summary>
    /// Rainbow spread (default: 3.0)
    /// </summary>
    public double Spread { get; init; } = Spread.ThrowIfOutOfRange(1, 100);

    /// <summary>
    /// Rainbow frequency (default: 0.1)
    /// </summary>
    public double Frequency { get; init; } = Frequency.ThrowIfOutOfRange(0.001, 1);

    /// <summary>
    /// Random seed, 0 = random (default: 0)
    /// </summary>
    public double Seed { get; init; } = Seed;
}

namespace Lolcat;

/// <summary>Style options for lolcat</summary>
/// <param name="EscapeSequence">Escape sequence used to generate colours (default: Ansi)</param>
/// <param name="TrimWindowOverflow">Trim if console window would overflow (default: true)</param>
/// <param name="Spread">Rainbow spread (default: 3.0)</param>
/// <param name="Frequency">Rainbow frequency (default: 0.1)</param>
/// <param name="Seed">Random seed, 0 = random (default: 0)</param>
/// <param name="Duration">Animation duration (default: 12s)</param>
/// <param name="Speed">Animation speed (default 20)</param>
public sealed record RainbowStyle(
    EscapeSequence EscapeSequence = EscapeSequence.Ansi,
    bool TrimWindowOverflow = true,
    double Spread = 3,
    double Frequency = .1,
    double Seed = 0,
    TimeSpan? Duration = null,
    double Speed = 20)
{
    /// <summary>
    /// Escape sequence used to generate colours (default: Ansi)
    /// </summary>
    public EscapeSequence EscapeSequence { get; init; } = EscapeSequence;

    /// <summary>
    /// Trim if console window would overflow (default: true)
    /// </summary>
    public bool TrimWindowOverflow { get; } = TrimWindowOverflow;

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

    /// <summary>
    /// Animation duration (default: 12s)
    /// </summary>
    public TimeSpan? Duration { get; } = Duration ?? TimeSpan.FromSeconds(12);

    /// <summary>
    /// Animation speed (default 20)
    /// </summary>
    public double Speed { get; } = Speed.ThrowIfZeroOrLess();
}

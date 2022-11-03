namespace Lolcat;

/// <summary>Style options for lolcat</summary>
/// <param name="EscapeSequence">Escape sequence used to generate colours (default: Ansi)</param>
/// <param name="Spread">Rainbow spread (default: <c>3.0</c>)</param>
/// <param name="Frequency">Rainbow frequency (default: <c>0.1</c>)</param>
/// <param name="Seed">Random seed, <c>0</c> = random (default: <c>0</c>)</param>
/// <param name="Animate">Enable psychedelics (default: false)</param>
/// <param name="Duration">Animation duration (default: 12s)</param>
/// <param name="Speed">Animation speed (default 20)</param>
public record RainbowStyle(
    EscapeSequence EscapeSequence = EscapeSequence.Ansi,
    double Spread = 3,
    double Frequency = .1,
    int Seed = 0,
    bool Animate = false,
    TimeSpan? Duration = null,
    double Speed = 20)
{
    /// <summary>
    /// Escape sequence used to generate colours (default: Ansi)
    /// </summary>
    public EscapeSequence EscapeSequence { get; init; } = EscapeSequence;

    /// <summary>
    /// Rainbow spread (default: <c>3.0</c>)
    /// </summary>
    public double Spread { get; init; } = Spread.ThrowIfOutOfRange(1, 100);

    /// <summary>
    /// Rainbow frequency (default: <c>0.1</c>)
    /// </summary>
    public double Frequency { get; init; } = Frequency.ThrowIfOutOfRange(0.001, 1);

    /// <summary>
    /// Random seed, <c>0</c> = random (default: <c>0</c>)
    /// </summary>
    public int Seed { get; init; } = Seed;

    /// <summary>
    /// Enable psychedelics (default: false)
    /// </summary>
    public bool Animate { get; } = Animate;

    /// <summary>
    /// Animation duration (default: 12s)
    /// </summary>
    public TimeSpan? Duration { get; } = Duration ?? TimeSpan.FromSeconds(12);

    /// <summary>
    /// Animation speed (default 20)
    /// </summary>
    public double Speed { get; } = Speed.ThrowIfZeroOrLess();
}

namespace Lolcat;

/// <summary>Style options for lolcat</summary>
/// <param name="EscapeSequence">Escape sequence used to generate colours (default: Ansi)</param>
/// <param name="Spread">Rainbow spread (default: <c>3.0</c>)</param>
/// <param name="Frequency">Rainbow frequency (default: <c>0.1</c>)</param>
/// <param name="Seed">Random seed, <c>0</c> = random (default: <c>0</c>).</param>
/// <param name="Animate">Enable psychedelics (default: false)</param>
/// <param name="Duration">Animation duration (default: 12s)</param>
/// <param name="Speed">Animation speed (default 20)</param>
public record RainbowStyle(
    EscapeSequence EscapeSequence = EscapeSequence.Ansi,
    double Spread = 3,
    double Frequency = .1,
    int Seed = 0,
    bool Animate = false,
    TimeSpan Duration = default,
    double Speed = 0)
{
    public EscapeSequence EscapeSequence { get; init; } = EscapeSequence;
    public double Spread { get; init; } = Spread.ThrowIfOutOfRange(1, 100);
    public double Frequency { get; init; } = Frequency.ThrowIfOutOfRange(0.001, 1);
    public int Seed { get; init; } = Seed;
    public bool Animate { get; init; } = Animate;
    public TimeSpan Duration { get; init; } = Duration == default ? TimeSpan.FromSeconds(12) : Duration;
    public double Speed { get; init; } = Speed <= 0 ? 0.1 : Speed;
}

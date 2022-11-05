namespace Lolcat;

/// <summary>Animation style options</summary>
/// <param name="ClearConsole">Clears the console on start and window resize (default: true)</param>
/// <param name="Duration">Animation duration (default: 12s)</param>
/// <param name="Speed">Animation speed (default 20)</param>
public sealed record AnimationStyle(
    bool ClearConsole = true,
    TimeSpan? Duration = null,
    double Speed = 20)
{
    /// <summary>
    /// Clears the console on start and window resize (default: true)
    /// </summary>
    /// <remarks>
    /// If this is set <c>false</c> expect strange things to happen on window resizing
    ///
    /// If you require more fine-grained control over animation and console window,
    /// use <see cref="Rainbow.Markup(string, double)"/>
    /// </remarks>
    public bool ClearConsole { get; } = ClearConsole;

    /// <summary>
    /// Animation duration (default: 12s)
    /// </summary>
    public TimeSpan? Duration { get; } = Duration ?? TimeSpan.FromSeconds(12);

    /// <summary>
    /// Animation speed (default 20)
    /// </summary>
    public double Speed { get; } = Speed.ThrowIfZeroOrLess();
}

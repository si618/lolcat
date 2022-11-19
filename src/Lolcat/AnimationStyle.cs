namespace Lolcat;

/// <summary>Animation style options</summary>
/// <param name="Duration">Animation duration (default: 12s)</param>
/// <param name="Speed">Animation speed (default 20)</param>
/// <param name="ClearConsole">Clears the console on start and window resize (default: true)</param>
/// <param name="StopOnResize">Stop animation if console window is resized (default: false)</param>
public sealed record AnimationStyle(
    TimeSpan? Duration = null,
    int Speed = 20,
    bool ClearConsole = true,
    bool PadToWindowSize = true,
    bool StopOnResize = false)
{
    /// <summary>
    /// Animation duration (default: 12s)
    /// </summary>
    public TimeSpan? Duration { get; } = Duration ?? TimeSpan.FromSeconds(12);

    /// <summary>
    /// Animation speed (default 20)
    /// </summary>
    public int Speed { get; } = Speed.ThrowIfLessThanOne();

    /// <summary>
    /// Clears the console on start and window resize (default: true)
    /// </summary>
    /// <remarks>
    /// If this is set <c>false</c> expect strange things to happen on window resizing unless
    /// <see cref="StopOnResize"/> is <c>true</c>
    /// </remarks>
    public bool ClearConsole { get; } = ClearConsole;

    /// <summary>
    /// Add padding or remove text to fill the current window size (default: true)
    /// </summary>
    public bool PadToWindowSize { get; } = PadToWindowSize;

    /// <summary>
    /// Stop animation if window is resized
    /// </summary>
    public bool StopOnResize { get; } = StopOnResize;
}

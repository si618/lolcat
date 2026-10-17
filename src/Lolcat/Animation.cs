namespace Lolcat;

/// <summary>
/// Rainbow animation
/// </summary>
public sealed class Animation
{
    private readonly TimeProvider _time;

    public Animation(Rainbow? rainbow = null, AnimationStyle? style = null)
        : this(TimeProvider.System, rainbow, style)
    {
    }

    internal Animation(TimeProvider time, Rainbow? rainbow = null, AnimationStyle? style = null)
    {
        _time = time;
        Rainbow = rainbow ?? new Rainbow();
        AnimationStyle = style ?? new AnimationStyle();
        Seed = Rainbow.GetStartingSeed();
    }

    /// <summary>
    /// The rainbow being animated
    /// </summary>
    public Rainbow Rainbow { get; }

    private AnimationStyle AnimationStyle { get; }
    private double Seed { get; set; }

    /// <summary>
    /// Animate <paramref name="text" /> as a rainbow using <see cref="Lolcat.AnimationStyle"/> and
    /// <see cref="RainbowStyle"/>
    /// </summary>
    /// <param name="text">The text to animate</param>
    /// <remarks>
    /// Console window resizing is not fully supported, expect strange things to happen :)
    /// </remarks>
    public void Animate(string text) => AnimateAsync(text).GetAwaiter().GetResult();

    /// <summary>
    /// Animate <paramref name="text" /> as a rainbow using <see cref="Lolcat.AnimationStyle"/> and
    /// <see cref="RainbowStyle"/>, until the duration elapses or the token is cancelled
    /// </summary>
    /// <param name="text">The text to animate</param>
    /// <param name="cancellationToken">Stops the animation</param>
    /// <remarks>
    /// Console window resizing is not fully supported, expect strange things to happen :)
    /// </remarks>
    public async Task AnimateAsync(string text, CancellationToken cancellationToken = default)
    {
        long start = _time.GetTimestamp();
        TimeSpan frame = TimeSpan.FromSeconds(1) / AnimationStyle.Speed;

        IConsole console = Rainbow.Console;
        int width = console.GetWindowWidth();
        int height = console.GetWindowHeight();
        string[] lines = text.ReplaceLineEndings().Split(Environment.NewLine);
        bool cursorVisible = console.GetCursorVisibility();

        if (AnimationStyle.ClearConsole)
        {
            console.Clear();
        }
        console.SetBufferSizeToCurrentWindow();
        console.SetCursorVisibility(visible: false);

        try
        {
            int cursorTop = console.GetCursorTop();

            while (_time.GetElapsedTime(start) <= AnimationStyle.Duration)
            {
                int currentWidth = console.GetWindowWidth();
                int currentHeight = console.GetWindowHeight();

                if (currentWidth != width || currentHeight != height)
                {
                    if (AnimationStyle.StopOnResize)
                    {
                        break;
                    }

                    width = currentWidth;
                    height = currentHeight;

                    console.SetBufferSizeToCurrentWindow();

                    if (AnimationStyle.ClearConsole)
                    {
                        console.Clear();
                    }
                }

                console.MoveCursorToTopLeft(cursorTop);

                WriteLines(lines, cursorTop);

                await Task.Delay(frame, _time, cancellationToken).ConfigureAwait(false);

                Seed += Rainbow.RainbowStyle.Spread;
            }
        }
        finally
        {
            console.SetCursorVisibility(cursorVisible);
        }
    }

    private void WriteLines(IEnumerable<string> lines, int cursorTop)
    {
        IConsole console = Rainbow.Console;

        Rainbow.Lines.Clear();
        Rainbow.Lines.AddRange(lines);

        if (AnimationStyle.PadToWindowSize)
        {
            AdjustLineHeight(cursorTop);
            AdjustLineWidth();
        }

        Rainbow.BuildLines(Seed);

        for (int i = 0; i < Rainbow.Lines.Count; i++)
        {
            string line = Rainbow.Lines[i];

            console.MoveCursorToStartOfLine();

            if (i + 1 < Rainbow.Lines.Count)
            {
                console.WriteLine(line);
            }
            else
            {
                console.Write(line);
            }
        }
    }

    private void AdjustLineHeight(int cursorTop)
    {
        int availableHeight = Rainbow.Console.GetWindowHeight() - cursorTop;

        List<string> lines = Rainbow.Lines;
        if (availableHeight <= 0 || availableHeight == lines.Count)
        {
            return;
        }

        if (availableHeight < lines.Count)
        {
            lines.RemoveRange(availableHeight, lines.Count - availableHeight);
        }
        else
        {
            int pad = availableHeight - lines.Count;
            lines.AddRange(new string[pad]);
        }
    }

    private void AdjustLineWidth()
    {
        int windowWidth = Rainbow.Console.GetWindowWidth();
        if (windowWidth <= 0)
        {
            return;
        }

        for (int i = 0; i < Rainbow.Lines.Count; i++)
        {
            var line = new StringBuilder(Rainbow.Lines[i]);

            for (int j = line.Length - 1; j >= 0; j--)
            {
                if (line.Length <= windowWidth)
                {
                    break;
                }

                bool pair = j - 1 > 0 && char.IsSurrogatePair(line[j - 1], line[j]);

                if (pair)
                {
                    j--;
                }

                line.Remove(j, pair ? 2 : 1);
            }

            Rainbow.Lines[i] = line.ToString().PadRight(windowWidth);
        }
    }
}

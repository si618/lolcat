namespace Lolcat;

/// <summary>
/// Rainbow animation
/// </summary>
public sealed class Animation
{
    public Animation(Rainbow? rainbow = null, AnimationStyle? style = null)
    {
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
    public void Animate(string text)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var console = Rainbow.Console;
        var width = console.GetWindowWidth();
        var height = console.GetWindowHeight();
        var lines = text.ReplaceLineEndings().Split(Environment.NewLine);
        var cursorVisible = console.GetCursorVisibility();

        if (AnimationStyle.ClearConsole)
        {
            console.Clear();
        }
        console.SetBufferSizeToCurrentWindow();
        console.SetCursorVisibility(false);

        var cursorTop = console.GetCursorTop();

        while (stopwatch.Elapsed <= AnimationStyle.Duration)
        {
            var currentWidth = console.GetWindowWidth();
            var currentHeight = console.GetWindowHeight();
            if (currentWidth != width || currentHeight != height)
            {
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

            Thread.Sleep((int)(1_000 / AnimationStyle.Speed));

            Seed += Rainbow.RainbowStyle.Spread;
        }

        console.SetCursorVisibility(cursorVisible);
    }

    private void WriteLines(IEnumerable<string> lines, int cursorTop)
    {
        var console = Rainbow.Console;

        Rainbow.Lines.Clear();
        Rainbow.Lines.AddRange(lines);

        AdjustLineHeight(cursorTop);
        AdjustLineWidth();

        Rainbow.BuildLines(Seed);

        for (var i = 0; i < Rainbow.Lines.Count; i++)
        {
            var line = Rainbow.Lines[i];

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
        var availableHeight = Rainbow.Console.GetWindowHeight() - cursorTop;

        var lines = Rainbow.Lines;
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
            var pad = availableHeight - lines.Count;
            lines.AddRange(new string[pad]);
        }
    }

    private void AdjustLineWidth()
    {
        var windowWidth = Rainbow.Console.GetWindowWidth();
        if (windowWidth <= 0) return;

        for (var i = 0; i < Rainbow.Lines.Count; i++)
        {
            var line = new StringBuilder(Rainbow.Lines[i]);

            for (var j = line.Length - 1; j >= 0; j--)
            {
                if (line.Length <= windowWidth) break;

                var pair = j - 1 > 0 && char.IsSurrogatePair(line[j - 1], line[j]);

                if (pair) j--;

                line.Remove(j, pair ? 2 : 1);
            }

            Rainbow.Lines[i] = line.ToString().PadRight(windowWidth);
        }
    }
}
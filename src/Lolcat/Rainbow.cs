namespace Lolcat;

/// <summary>
/// Class to convert plain text to static or animated rainbow
/// </summary>
public sealed class Rainbow
{
    public Rainbow(RainbowStyle? rainbowStyle = null)
    {
        RainbowStyle = rainbowStyle ?? new RainbowStyle();
        Console = RainbowStyle.EscapeSequence == EscapeSequence.Ansi
            ? new SystemConsole()
            : new SpectreConsole();
    }

    internal Rainbow(IConsole console, RainbowStyle? rainbowStyle = null) : this(rainbowStyle)
    {
        Console = console;
    }

    public RainbowStyle RainbowStyle { get; }

    /// <summary>
    /// Markup <paramref name="text" /> to a rainbow using <see cref="RainbowStyle"/>
    /// </summary>
    public string Markup(string text, double seed)
    {
        _lines.Clear();
        _lines.AddRange(text.ReplaceLineEndings().Split(Environment.NewLine));

        BuildLines(seed);

        return string.Join(Environment.NewLine, _lines);
    }

    /// <summary>
    /// Markup <paramref name="text" /> to a rainbow using <see cref="RainbowStyle"/>
    /// </summary>
    public string Markup(string text) => Markup(text, GetStartingSeed());

    /// <summary>
    /// Markup <paramref name="text" /> to a rainbow using <see cref="RainbowStyle"/>,
    /// followed by the current line terminator
    /// </summary>
    public string MarkupLine(string text) => Markup(text) + Environment.NewLine;

    /// <summary>
    /// Animate <paramref name="text" /> as a rainbow using <see cref="RainbowStyle"/>
    /// </summary>
    /// <param name="text">The text to animate</param>
    public void Animate(string text)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var seed = GetStartingSeed();
        var cursorTop = Console.GetCursorTop();
        var cursorVisible = Console.GetCursorVisibility();
        var lines = text.ReplaceLineEndings().Split(Environment.NewLine);

        Console.SetCursorVisibility(false);

        while (stopwatch.Elapsed <= RainbowStyle.Duration)
        {
            Console.MoveCursorToTop(cursorTop);
            cursorTop = Console.GetCursorTop();

            _lines.Clear();
            _lines.AddRange(lines);

            BuildLines(seed);

            foreach (var line in _lines)
            {
                Console.MoveCursorToStartOfLine();
                Console.WriteLine(line);
            }

            Thread.Sleep((int)(1_000 / RainbowStyle.Speed));

            seed += RainbowStyle.Spread;
        }

        Console.SetCursorVisibility(cursorVisible);
    }

    private IConsole Console { get; }

    private readonly List<string> _lines = new();
    private readonly StringBuilder _line = new();

    private double GetStartingSeed()
    {
        var random = RainbowStyle.Seed == 0 ? new Random() : new Random((int)RainbowStyle.Seed);

        return RainbowStyle.Seed == 0 ? random.Next(0, 255) : Convert.ToDouble(RainbowStyle.Seed);
    }

    private void BuildLines(double seed)
    {
        if (RainbowStyle.TrimWindowOverflow)
        {
            TrimLines();
        }

        for (var i = 0; i < _lines.Count; i++)
        {
            seed++;

            // No point building an empty or blank line
            if (string.IsNullOrWhiteSpace(_lines[i])) continue;

            BuildLine(_lines[i], seed + RainbowStyle.Spread);

            _lines[i] = _line.ToString();
        }
    }

    private void TrimLines()
    {
        TrimLineHeight();
        TrimLineWidth();
    }

    private void TrimLineHeight()
    {
        var availableHeight = Console.GetWindowHeight() - Console.GetCursorTop();
        if (availableHeight < 1 || availableHeight >= _lines.Count) return;

        availableHeight -= 1;
        _lines.RemoveRange(availableHeight, _lines.Count - availableHeight);
    }

    private void TrimLineWidth()
    {
        var windowWidth = Console.GetWindowWidth() - 1;
        if (windowWidth <= 0) return;

        for (var i = 0; i < _lines.Count; i++)
        {
            var line = new StringBuilder(_lines[i]);

            for (var j = line.Length - 1; j >= 0; j--)
            {
                var pair = j - 1 > 0 && char.IsSurrogatePair(line[j - 1], line[j]);

                if (pair) j--;

                line.Remove(j, pair ? 2 : 1);

                if (line.Length - 1 <= windowWidth) break;
            }

            _lines[i] = line.ToString();
        }
    }

    private void BuildLine(string line, double seed)
    {
        _line.Clear();

        for (var i = 0; i < line.Length; i++)
        {
            var n = (seed + i) / RainbowStyle.Spread;

            var red = (int)(Math.Sin(RainbowStyle.Frequency * n) * 127 + 128);
            var green = (int)(Math.Sin(RainbowStyle.Frequency * n + 2 * Math.PI / 3) * 127 + 128);
            var blue = (int)(Math.Sin(RainbowStyle.Frequency * n + 4 * Math.PI / 3) * 127 + 128);

            var pair = i + 1 < line.Length && char.IsSurrogatePair(line[i], line[i + 1]);
            var @char = pair ? line[i] + line[++i].ToString() : line[i].ToString();

            _line.AppendFormat(Console.Format, red, green, blue, @char);
        }
    }
}
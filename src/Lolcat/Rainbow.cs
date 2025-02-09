namespace Lolcat;

/// <summary>
/// Convert plain text to a rainbow
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

    public static Rainbow WithStyle(RainbowStyle rainbowStyle) => new(rainbowStyle);

    internal Rainbow(IConsole console, RainbowStyle? rainbowStyle = null) : this(rainbowStyle)
    {
        Console = console;
    }

    public IConsole Console { get; }

    /// <summary>
    /// Rainbow style properties
    /// </summary>
    public RainbowStyle RainbowStyle { get; }

    /// <summary>
    /// Markup <paramref name="text" /> to a rainbow using <see cref="RainbowStyle"/>
    /// </summary>
    public string Markup(string text, double seed)
    {
        Lines.Clear();
        Lines.AddRange(text.ReplaceLineEndings().Split(Environment.NewLine));

        BuildLines(seed);

        return string.Join(Environment.NewLine, Lines);
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
    /// Write markup of <paramref name="text" /> to <see cref="Console"/>
    /// </summary>
    public void WriteWithMarkup(string text) => Console.Write(Markup(text));

    /// <summary>
    /// Write markup of <paramref name="text" /> to <see cref="Console"/>
    /// </summary>
    public void WriteWithMarkup(string text, double seed) => Console.Write(Markup(text, seed));

    /// <summary>
    /// Write markup of <paramref name="text" /> to <see cref="Console"/>, followed by the current
    /// line terminator
    /// </summary>
    public void WriteLineWithMarkup(string text) => Console.WriteLine(Markup(text));

    /// <summary>
    /// Write markup of <paramref name="text" /> to <see cref="Console"/>, followed by the current
    /// line terminator
    /// </summary>
    public void WriteLineWithMarkup(string text, double seed) =>
        Console.WriteLine(Markup(text, seed));

    internal List<string> Lines { get; } = [];
    private StringBuilder Line { get; } = new();

    internal double GetStartingSeed()
    {
        var random = RainbowStyle.Seed == 0 ? new Random() : new Random((int)RainbowStyle.Seed);

        return RainbowStyle.Seed == 0 ? random.Next(0, 255) : Convert.ToDouble(RainbowStyle.Seed);
    }

    internal void BuildLines(double seed)
    {
        for (var i = 0; i < Lines.Count; i++)
        {
            seed++;

            // No point building an empty or blank line
            if (string.IsNullOrWhiteSpace(Lines[i])) continue;

            BuildLine(Lines[i], seed + RainbowStyle.Spread);

            Lines[i] = Line.ToString();
        }
    }

    private void BuildLine(string line, double seed)
    {
        Line.Clear();

        for (var i = 0; i < line.Length; i++)
        {
            var n = (seed + i) / RainbowStyle.Spread;

            var red = (int)(Math.Sin(RainbowStyle.Frequency * n) * 127 + 128);
            var green = (int)(Math.Sin(RainbowStyle.Frequency * n + 2 * Math.PI / 3) * 127 + 128);
            var blue = (int)(Math.Sin(RainbowStyle.Frequency * n + 4 * Math.PI / 3) * 127 + 128);

            var pair = i + 1 < line.Length && char.IsSurrogatePair(line[i], line[i + 1]);
            var @char = pair ? line[i] + line[++i].ToString() : line[i].ToString();

            if (RainbowStyle.EscapeSequence == EscapeSequence.Spectre)
            {
                @char = @char.EscapeMarkup();
            }

            Line.AppendFormat(Console.Format, red, green, blue, @char);
        }
    }
}
using System.Globalization;

namespace Lolcat;

/// <summary>
/// Convert plain text to a rainbow
/// </summary>
public sealed class Rainbow
{
    private readonly bool _escape;

    public Rainbow(RainbowStyle? rainbowStyle = null)
    {
        RainbowStyle = rainbowStyle ?? new RainbowStyle();

        if (RainbowStyle.EscapeSequence == EscapeSequence.Ansi)
        {
            Console = new SystemConsole();
        }
        else
        {
            Console = new SpectreConsole();
            _escape = true;
        }
    }

    internal Rainbow(IConsole console, RainbowStyle? rainbowStyle = null) : this(rainbowStyle) => Console = console;

    public static Rainbow WithStyle(RainbowStyle rainbowStyle) => new(rainbowStyle);

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

    /// <summary>
    /// Animate <paramref name="text" /> as a rainbow using <paramref name="style"/> and
    /// <see cref="RainbowStyle"/>
    /// </summary>
    /// <param name="text">The text to animate</param>
    /// <param name="style">The animation style</param>
    /// <remarks>
    /// Console window resizing is not fully supported, expect strange things to happen :)
    /// </remarks>
    public void Animate(string text, AnimationStyle? style = null) =>
        new Animation(this, style).Animate(text);

    internal List<string> Lines { get; } = [];
    private StringBuilder Line { get; } = new();

    internal double GetStartingSeed()
    {
        Random random = RainbowStyle.Seed == 0 ? new Random() : new Random((int)RainbowStyle.Seed);

        return RainbowStyle.Seed == 0 ? random.Next(0, 255) : Convert.ToDouble(RainbowStyle.Seed);
    }

    internal void BuildLines(double seed)
    {
        for (int i = 0; i < Lines.Count; i++)
        {
            seed++;

            // No point building an empty or blank line
            if (string.IsNullOrWhiteSpace(Lines[i]))
            {
                continue;
            }

            BuildLine(Lines[i], seed + RainbowStyle.Spread);

            Lines[i] = Line.ToString();
        }
    }

    private void BuildLine(string line, double seed)
    {
        Line.Clear();

        for (int i = 0; i < line.Length; i++)
        {
            double n = (seed + i) / RainbowStyle.Spread;

            double angle = RainbowStyle.Frequency * n;
            int red = (int)((Math.Sin(angle) * 127) + 128);
            int green = (int)((Math.Sin(angle + (2 * Math.PI / 3)) * 127) + 128);
            int blue = (int)((Math.Sin(angle + (4 * Math.PI / 3)) * 127) + 128);

            bool pair = i + 1 < line.Length && char.IsSurrogatePair(line[i], line[i + 1]);
            string @char = pair ? line[i] + line[++i].ToString() : line[i].ToString();

            if (_escape)
            {
                @char = Spectre.Console.Markup.Escape(@char);
            }

            // Machine-facing: escape sequences need invariant digits.
            Line.AppendFormat(CultureInfo.InvariantCulture, Console.Format, red, green, blue, @char);
        }
    }
}

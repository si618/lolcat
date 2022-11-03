namespace Lolcat;

/// <summary>
/// Add rainbow style to text
/// </summary>
public class Rainbow
{
    private const char Escape = (char)27;
    private const string Spectre = "[rgb({0},{1},{2})]{3}[/]";
    private static readonly string Ansi = $"{Escape}[38;2;{{0}};{{1}};{{2}};1m{{3}}{Escape}[0m";

    private ILolcatConsole Console { get; }

    public RainbowStyle RainbowStyle { get; set; }

    public Rainbow(RainbowStyle? rainbowStyle = null)
    {
        RainbowStyle = rainbowStyle ?? new RainbowStyle();
        Console = RainbowStyle.EscapeSequence == EscapeSequence.Ansi
            ? new LolcatAnsiConsole()
            : new LolcatSpectreConsole();
    }

    public Rainbow(ILolcatConsole console, RainbowStyle rainbowStyle)
        : this(rainbowStyle)
    {
        Console = console;
    }

    /// <summary>
    /// Markup <paramref name="text" /> to a rainbow using defined <see cref="RainbowStyle"/>
    /// </summary>
    public string Markup(string text) =>
        string.Join(Environment.NewLine, BuildText(text, GetStartingSeed()));

    /// <summary>
    /// Markup <paramref name="text" /> to a rainbow using defined <see cref="RainbowStyle"/>,
    /// follow by the current line terminator
    /// </summary>
    public string MarkupLine(string text) => Markup(text) + Environment.NewLine;

    /// <summary>
    /// Animate <paramref name="text" /> as a rainbow using defined <see cref="RainbowStyle"/>
    /// </summary>
    public void Animate(string text)
    {
        if (!RainbowStyle.Animate)
        {
            return;
        }

        var cursorVisible = Console.GetCursorVisibility();
        Console.SetCursorVisibility(false);
        Console.Clear();

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var seed = GetStartingSeed();

        while (stopwatch.Elapsed <= RainbowStyle.Duration)
        {
            Console.MoveToTopRow();

            foreach (var line in BuildText(text, seed))
            {
                Console.MoveToStartOfLine();
                Console.WriteLine(line);
            }

            Thread.Sleep((int)(1_000 / RainbowStyle.Speed));

            seed += Convert.ToInt32(RainbowStyle.Spread);
        }

        Console.SetCursorVisibility(cursorVisible);
    }

    private double GetStartingSeed()
    {
        var random = RainbowStyle.Seed == 0 ? new Random() : new Random(RainbowStyle.Seed);

        return RainbowStyle.Seed == 0 ? random.Next(0, 255) : Convert.ToDouble(RainbowStyle.Seed);

    }
    private string[] BuildText(string text, double seed)
    {
        var lines = CollectionsMarshal
            .AsSpan(text.ReplaceLineEndings().Split(Environment.NewLine).ToList());

        return BuildLines(lines, seed);
    }

    private string[] BuildLines(Span<string> lines, double seed)
    {
        var output = new List<string>();
        var format = RainbowStyle.EscapeSequence == EscapeSequence.Ansi ? Ansi : Spectre;

        foreach (var line in lines)
        {
            seed++;

            // No point building an empty line
            if (line.Length == 0 && output.Count > 0)
            {
                output.Add(string.Empty);
                continue;
            }

            output.Add(BuildLine(line, format, seed + RainbowStyle.Spread));
        }

        return output.ToArray();
    }

    private string BuildLine(string line, string format, double seed)
    {
        var output = new StringBuilder();

        for (var i = 0; i < line.Length; i++)
        {
            var n = (seed + i) / RainbowStyle.Spread;
            var markup = line[i].ToString();

            if (i + 1 < line.Length &&
                char.IsSurrogatePair(line[i], line[i + 1]))
            {
                markup += line[++i].ToString();
            }

            var red = (int)(Math.Sin(RainbowStyle.Frequency * n) * 127 + 128);
            var green = (int)(Math.Sin(RainbowStyle.Frequency * n + 2 * Math.PI / 3) * 127 + 128);
            var blue = (int)(Math.Sin(RainbowStyle.Frequency * n + 4 * Math.PI / 3) * 127 + 128);

            output.AppendFormat(format, red, green, blue, markup);
        }

        return output.ToString();
    }
}
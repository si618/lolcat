namespace Lolcat;

public class Rainbow
{
    private const char Escape = (char)27;
    private const string AnsiFormat = "{0}[38;2;{1};{2};{3};1m{4}{0}[0m";
    private const string SpectreFormat = "[rgb({0},{1},{2})]{3}[/]";

    public RainbowStyle Style { get; set; }

    public Rainbow(RainbowStyle? style = null)
    {
        Style = style ?? new RainbowStyle();
    }

    /// <summary>
    /// Convert <paramref name="text" /> to a rainbow using defined <see cref="Style"/>
    /// </summary>
    public string Convert(string text)
    {
        var random = Style.Seed == 0 ? new Random() : new Random(Style.Seed);
        var seed = random.Next(255);
        var lines = text.ReplaceLineEndings().Split(Environment.NewLine);
        var output = new StringBuilder();

        foreach (var line in lines)
        {
            seed++;

            var length = line.Length;
            if (length == 0 && output.Length > 0)
            {
                // Empty line
                output.AppendLine();
                continue;
            }

            var s = seed;

            for (var i = 0; i < length; i++)
            {
                var n = s + i / Style.Spread;
                var c = line[i];

                if (i < length - 1 && char.IsSurrogatePair(c, line[i + 1]))
                {
                    c += line[i + 1];
                    i++;
                }

                var red = (int)(Math.Sin(Style.Frequency * n) * 127 + 128);
                var green = (int)(Math.Sin(Style.Frequency * n + 2 * Math.PI / 3) * 127 + 128);
                var blue = (int)(Math.Sin(Style.Frequency * n + 4 * Math.PI / 3) * 127 + 128);

                if (Style.EscapeSequence == EscapeSequence.Ansi)
                {
                    output.AppendFormat(AnsiFormat, Escape, red, green, blue, c);
                }
                else
                {
                    output.AppendFormat(SpectreFormat, red, green, blue, c);
                }
            }

            output.AppendLine();
        }

        return output.ToString().TrimEnd(Environment.NewLine.ToCharArray());
    }
}

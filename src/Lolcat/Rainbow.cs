namespace Lolcat;

public class Rainbow
{
    private const char Escape = (char)27;
    private const string AnsiFormat = "{0}[38;2;{1};{2};{3};1m{4}{0}[0m";
    private const string SpectreFormat = "[rgb({0},{1},{2})]{3}[/]";

    public RainbowStyle RainbowStyle { get; set; }

    public Rainbow(RainbowStyle? rainbowStyle = null)
    {
        RainbowStyle = rainbowStyle ?? new RainbowStyle();

        if (RainbowStyle.Enabled)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convert <paramref name="text" /> to a rainbow using defined <see cref="RainbowStyle"/>
    /// </summary>
    public string Convert(string text)
    {
        var random = RainbowStyle.Seed == 0 ? new Random() : new Random(RainbowStyle.Seed);
        var seed = random.Next(255);
        var lines = text.ReplaceLineEndings().Split(Environment.NewLine);
        var output = new StringBuilder();

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            seed++;

            var length = line.Length;
            if (length == 0 && output.Length > 0)
            {
                // Empty line
                output.AppendLine();
                continue;
            }

            var s = seed;

            for (var j = 0; j < length; j++)
            {
                var n = s + j / RainbowStyle.Spread;
                var c = line[j];

                if (j < length - 1 && char.IsSurrogatePair(c, line[j + 1]))
                {
                    c += line[j + 1];
                    j++;
                }

                var red = (int)(Math.Sin(RainbowStyle.Frequency * n) * 127 + 128);
                var green = (int)(Math.Sin(RainbowStyle.Frequency * n + 2 * Math.PI / 3) * 127 + 128);
                var blue = (int)(Math.Sin(RainbowStyle.Frequency * n + 4 * Math.PI / 3) * 127 + 128);

                if (RainbowStyle.EscapeSequence == EscapeSequence.Ansi)
                {
                    output.AppendFormat(AnsiFormat, Escape, red, green, blue, c);
                }
                else
                {
                    output.AppendFormat(SpectreFormat, red, green, blue, c);
                }
            }

            // Don't append at the end
            if (i + 1 < lines.Length)
            {
                output.AppendLine();
            }
        }

        return output.ToString();
    }
}

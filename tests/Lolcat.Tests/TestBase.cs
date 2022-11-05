namespace Lolcat.Tests;

public abstract class TestBase
{
    // ReSharper disable once InconsistentNaming
    protected static readonly string NL = Environment.NewLine;

    protected const double Seed = 42;

    protected MockConsole MockConsole { get; set; } = new();

    protected string RemoveMarkupFromLine(string line)
    {
        var cleaned = line.RemoveMarkup();

        if (string.IsNullOrEmpty(cleaned))
        {
            // RemoveMarkup returns empty string if there is whitespace
            cleaned = " ".PadRight(MockConsole.GetWindowWidth());
        }

        return cleaned;
    }

    protected static int CountSurrogatePairs(string line)
    {
        var chars = line.ToCharArray();

        var pairs = chars
            .Where((c, i) => i + 1 < chars.Length &&
                char.IsSurrogatePair(c, chars[i + 1]))
            .Count();

        return pairs;
    }
}

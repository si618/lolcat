namespace Lolcat.Tests;

public abstract class TestBase
{
    // ReSharper disable once InconsistentNaming
    protected static readonly string NL = Environment.NewLine;

    protected const double Seed = 42;

    protected MockConsole MockConsole { get; set; } = new();

    protected string RemoveMarkupAndPadRight(string text)
    {
        var cleaned = text.RemoveMarkup();

        if (string.IsNullOrEmpty(cleaned))
        {
            // RemoveMarkup returns empty string if there is whitespace
            cleaned = " ".PadRight(MockConsole.GetWindowWidth());
        }

        return cleaned;
    }

    protected static int CountSurrogatePairs(string text)
    {
        var chars = text.ToCharArray();

        var pairs = chars
            .Where((c, i) => i + 1 < chars.Length &&
                char.IsSurrogatePair(c, chars[i + 1]))
            .Count();

        return pairs;
    }
}

namespace Lolcat.Tests;

public static class Extensions
{
    public static TimeSpan Milliseconds(this int milliseconds)
    {
        return TimeSpan.FromMilliseconds(milliseconds);
    }

    public static string RemoveMarkupAndPadRight(this string text, int windowWidth)
    {
        var cleaned = text.RemoveMarkup();

        if (string.IsNullOrEmpty(cleaned))
        {
            // RemoveMarkup returns empty string if there is whitespace
            cleaned = " ".PadRight(windowWidth);
        }

        return cleaned;
    }
}

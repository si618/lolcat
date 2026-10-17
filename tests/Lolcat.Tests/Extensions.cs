namespace Lolcat.Tests;

public static class Extensions
{
    extension(string text)
    {
        public string RemoveMarkupAndPadRight(int windowWidth)
        {
            string cleaned = text.RemoveMarkup();

            if (string.IsNullOrEmpty(cleaned))
            {
                // RemoveMarkup returns empty string if there is whitespace
                cleaned = " ".PadRight(windowWidth);
            }

            return cleaned;
        }
    }
}

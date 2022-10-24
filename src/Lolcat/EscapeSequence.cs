namespace Lolcat;

public enum EscapeSequence
{
    /// <summary>
    /// Use standard ANSI escape sequence - https://en.wikipedia.org/wiki/ANSI_escape_code#24-bit
    /// </summary>
    Ansi,

    /// <summary>
    /// Use Spectre.Console escape sequence - https://spectreconsole.net/markup#colors
    /// </summary>
    Spectre
}

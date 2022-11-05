namespace Lolcat;

internal sealed class SystemConsole : IConsole
{
    private const char Esc = (char)27;
    internal static readonly string AnsiFormat = $"{Esc}[38;2;{{0}};{{1}};{{2}};1m{{3}}{Esc}[0m";

    public string Format { get; init; } = AnsiFormat;

    public int GetCursorTop() => Console.CursorTop;

    public int GetWindowHeight() => Console.WindowHeight;

    public int GetWindowWidth() => Console.WindowWidth;

    public bool GetCursorVisibility() => OperatingSystem.IsWindows() && Console.CursorVisible;

    public void MoveCursorToTop(int top) => Console.CursorTop = Console.BufferHeight > top
        ? top
        : Console.BufferHeight;

    public void MoveCursorToStartOfLine()
    {
        if (Console.BufferWidth < 0)
        {
            return;
        }
        try
        {
            Console.CursorLeft = 0;
        }
        catch (ArgumentOutOfRangeException)
        {
            // Let the next update set cursor
        }
    }

    public void SetCursorVisibility(bool visible) => Console.CursorVisible = visible;

    public void WriteLine(string text) => Console.WriteLine(text);
}

namespace Lolcat;

internal sealed class SpectreConsole : IConsole
{
    public const string SpectreFormat = "[rgb({0},{1},{2})]{3}[/]";

    public string Format { get; init; } = SpectreFormat;

    public int GetCursorTop() => Console.CursorTop;

    public bool GetCursorVisibility() => OperatingSystem.IsWindows() && Console.CursorVisible;

    public int GetWindowHeight() => Console.WindowHeight;

    public int GetWindowWidth() => Console.WindowWidth;

    public void MoveCursorToTop(int top)
    {
        try
        {
            Console.CursorTop = Console.BufferHeight > top ? top : Console.BufferHeight;
        }
        catch (ArgumentOutOfRangeException)
        {
            // Let the next update set cursor
        }
    }

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

    public void WriteLine(string text) => Spectre.Console.AnsiConsole.MarkupLine(text);
}

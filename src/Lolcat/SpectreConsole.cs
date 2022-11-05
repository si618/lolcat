namespace Lolcat;

internal sealed class SpectreConsole : IConsole
{
    public const string SpectreFormat = "[rgb({0},{1},{2})]{3}[/]";

    public string Format { get; init; } = SpectreFormat;

    public void Clear() => AnsiConsole.Clear();

    public int GetCursorTop() => Console.CursorTop;

    public bool GetCursorVisibility() => OperatingSystem.IsWindows() && Console.CursorVisible;

    public int GetWindowHeight() => Console.WindowHeight;

    public int GetWindowWidth() => Console.WindowWidth;

    public void MoveCursorToTopLeft(int top)
    {
        try
        {
            Console.SetCursorPosition(0, top);
        }
        catch
        {
            // Window being resized; let the next update set cursor position
        }
    }

    public void MoveCursorToStartOfLine()
    {
        try
        {
            Console.CursorLeft = 0;
        }
        catch
        {
            // Window being resized; let the next update set cursor position
        }
    }

    public void SetBufferSizeToCurrentWindow()
    {
        try
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetBufferSize(GetWindowWidth(), GetWindowHeight());
            }
        }
        catch
        {
            // Window being resized; let the next update set cursor position
        }
    }

    public void SetCursorVisibility(bool visible) => Console.CursorVisible = visible;

    public void Write(string text) => AnsiConsole.Markup(text);

    public void WriteLine(string text) => AnsiConsole.MarkupLine(text);
}

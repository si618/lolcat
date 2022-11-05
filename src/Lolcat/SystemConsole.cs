namespace Lolcat;

internal sealed class SystemConsole : IConsole
{
    private const char Esc = (char)27;
    internal static readonly string AnsiFormat = $"{Esc}[38;2;{{0}};{{1}};{{2}};1m{{3}}{Esc}[0m";

    public string Format { get; init; } = AnsiFormat;

    public void Clear() => Console.Clear();

    public int GetCursorTop() => Console.CursorTop;

    public int GetWindowHeight() => Console.WindowHeight;

    public int GetWindowWidth() => Console.WindowWidth;

    public bool GetCursorVisibility() => OperatingSystem.IsWindows() && Console.CursorVisible;

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

    public void Write(string text) => Console.Write(text);

    public void WriteLine(string text) => Console.WriteLine(text);
}

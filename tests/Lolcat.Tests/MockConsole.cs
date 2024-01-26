namespace Lolcat.Tests;

/// <summary>
/// Mock console used for tests and benchmarks, as a console is not typically available
/// </summary>
public sealed class MockConsole(
    EscapeSequence escapeSequence = EscapeSequence.Ansi,
    bool cursorVisibility = true,
    int windowHeight = 25,
    int windowWidth = 80,
    int cursorTop = 0,
    int cursorLeft = 0,
    string text = "")
    : IConsole
{
    public string Format { get; init; } = escapeSequence == EscapeSequence.Ansi
        ? SystemConsole.AnsiFormat
        : SpectreConsole.SpectreFormat;

    public void Clear() => text = string.Empty;

    public int GetCursorTop() => cursorTop;

    public int GetCursorLeft() => cursorLeft;

    public bool GetCursorVisibility() => cursorVisibility;

    public int GetWindowHeight() => windowHeight;

    public int GetWindowWidth() => windowWidth;

    public void MoveCursorToTopLeft(int top)
    {
        cursorTop = top;
        cursorLeft = 0;
    }

    public void MoveCursorToStartOfLine() => cursorLeft = 0;

    public void SetBufferSizeToCurrentWindow()
    {
    }

    public void SetCursorVisibility(bool visible) => cursorVisibility = visible;

    public void Write(string txt) => text = txt;

    public void WriteLine(string txt) => text = txt + Environment.NewLine;
}

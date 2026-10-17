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
    int cursorLeft = 0)
    : IConsole
{
    private bool _cursorVisibility = cursorVisibility;
    private int _cursorTop = cursorTop;
    private int _cursorLeft = cursorLeft;

    public string Format { get; init; } = escapeSequence == EscapeSequence.Ansi
        ? SystemConsole.AnsiFormat
        : SpectreConsole.SpectreFormat;

    /// <summary>
    /// Number of lines written, one per line in each animation frame
    /// </summary>
    public int LinesWritten { get; private set; }

    public void Clear()
    {
    }

    public int GetCursorTop() => _cursorTop;

    public int GetCursorLeft() => _cursorLeft;

    public bool GetCursorVisibility() => _cursorVisibility;

    public int GetWindowHeight() => windowHeight;

    public int GetWindowWidth() => windowWidth;

    public void MoveCursorToTopLeft(int top)
    {
        _cursorTop = top;
        _cursorLeft = 0;
    }

    public void MoveCursorToStartOfLine() => _cursorLeft = 0;

    public void SetBufferSizeToCurrentWindow()
    {
    }

    public void SetCursorVisibility(bool visible) => _cursorVisibility = visible;

    public void Write(string text) => LinesWritten++;

    public void WriteLine(string text) => LinesWritten++;
}

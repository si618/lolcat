namespace Lolcat.Tests;

/// <summary>
/// Mock console used for tests and benchmarks, as a console is not typically available
/// </summary>
public sealed class MockConsole : IConsole
{
    private bool _cursorVisibility;
    private readonly int _windowHeight;
    private readonly int _windowWidth;
    private int _cursorTop;
    private int _cursorLeft;

    private string _text;

    public MockConsole(
        EscapeSequence escapeSequence = EscapeSequence.Ansi,
        bool cursorVisibility = true,
        int windowHeight = 25,
        int windowWidth = 80,
        int cursorTop = 0,
        int cursorLeft = 0,
        string text = "")
    {
        _cursorVisibility = cursorVisibility;
        _windowHeight = windowHeight;
        _windowWidth = windowWidth;
        _cursorTop = cursorTop;
        _cursorLeft = cursorLeft;
        _text = text;
        Format = escapeSequence == EscapeSequence.Ansi
            ? SystemConsole.AnsiFormat
            : SpectreConsole.SpectreFormat;
    }

    public string Format { get; init; }

    public void Clear() => _text = string.Empty;

    public int GetCursorTop() => _cursorTop;

    public int GetCursorLeft() => _cursorLeft;

    public bool GetCursorVisibility() => _cursorVisibility;

    public int GetWindowHeight() => _windowHeight;

    public int GetWindowWidth() => _windowWidth;

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

    public void Write(string text) => _text = text;

    public void WriteLine(string text) => _text = text + Environment.NewLine;
}

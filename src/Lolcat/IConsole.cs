namespace Lolcat;

public interface IConsole
{
    // ReSharper disable once UnusedMember.Global
    string Format { get; init; }

    void Clear();
    int GetCursorTop();
    bool GetCursorVisibility();
    int GetWindowHeight();
    int GetWindowWidth();
    void MoveCursorToTopLeft(int top);
    void MoveCursorToStartOfLine();
    void SetBufferSizeToCurrentWindow();
    void SetCursorVisibility(bool visible);
    void Write(string text);
    void WriteLine(string text);
}

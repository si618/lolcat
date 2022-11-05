namespace Lolcat;

internal interface IConsole
{
    // ReSharper disable once UnusedMember.Global
    string Format { get; init; }

    int GetCursorTop();
    bool GetCursorVisibility();
    int GetWindowHeight();
    int GetWindowWidth();
    void MoveCursorToTop(int top);
    void MoveCursorToStartOfLine();
    void SetCursorVisibility(bool visible);
    void WriteLine(string text);
}

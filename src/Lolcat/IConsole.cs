namespace Lolcat;

public interface IConsole
{
    // ReSharper disable once UnusedMember.Global
    public string Format { get; init; }

    public void Clear();
    public int GetCursorTop();
    public bool GetCursorVisibility();
    public int GetWindowHeight();
    public int GetWindowWidth();
    public void MoveCursorToTopLeft(int top);
    public void MoveCursorToStartOfLine();
    public void SetBufferSizeToCurrentWindow();
    public void SetCursorVisibility(bool visible);
    public void Write(string text);
    public void WriteLine(string text);
}

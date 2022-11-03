namespace Lolcat;

public interface ILolcatConsole
{
    void Clear();
    bool GetCursorVisibility();
    void MoveToStartOfLine();
    void MoveToTopRow();
    void SetCursorVisibility(bool visible);
    void WriteLine(string text);
}

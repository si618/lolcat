namespace Lolcat;

public class LolcatAnsiConsole : ILolcatConsole
{
    public void Clear() => Console.Clear();

    public bool GetCursorVisibility() => OperatingSystem.IsWindows() && Console.CursorVisible;

    public void MoveToStartOfLine() => Console.CursorLeft = 0;

    public void MoveToTopRow() => Console.CursorTop = 0;

    public void SetCursorVisibility(bool visible) => Console.CursorVisible = visible;

    public void WriteLine(string text) => Console.WriteLine(text);
}

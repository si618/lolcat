using Lolcat;
using Spectre.Console;

AnsiConsole.Clear();

// Ansi output is the default escape sequence
var text = "Someday we'll find it, the rainbow connection";
var style = new RainbowStyle();
var lolcat = new Rainbow(style);
var ansi = lolcat.Convert(text);
Console.WriteLine(ansi);

// Spectre.Console output
text = "The lovers, the dreamers and me";
lolcat.Style = style with
{
    EscapeSequence = EscapeSequence.Spectre,
    Frequency = 1,
    Spread = 5,
    Seed = 42
};
var spectre = lolcat.Convert(text);
AnsiConsole.MarkupLine(spectre);

// Readme screenshot
//Console.Clear();
//lolcat.Style = new RainbowStyle();
//Console.WriteLine(lolcat.Convert(File.ReadAllText("./Program.cs")));

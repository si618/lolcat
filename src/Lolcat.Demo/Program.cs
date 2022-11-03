using Lolcat;
using Spectre.Console;

AnsiConsole.Clear();

var text = "Someday we'll find it, the rainbow connection";

// Ansi is the default escape sequence
var ansi = new RainbowStyle();
var lolcat = new Rainbow(ansi);
var markup = lolcat.Markup(text);
Console.WriteLine(markup);

text = "The lovers, the dreamers and me";

// Spectre.Console escape sequence is also available
lolcat.RainbowStyle = ansi with
{
    EscapeSequence = EscapeSequence.Spectre,
    Frequency = 1,
    Spread = 5,
    Seed = 42
};
var spectre = lolcat.Markup(text);
AnsiConsole.MarkupLine(spectre);

// Ouroboros 😎
AnsiConsole.Clear();
lolcat.RainbowStyle = new RainbowStyle();
markup = lolcat.Markup(File.ReadAllText("./Program.cs"));
Console.WriteLine(markup);

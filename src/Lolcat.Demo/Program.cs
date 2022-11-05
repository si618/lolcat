using Lolcat;
using Spectre.Console;

AnsiConsole.Clear();

var text = "Someday we'll find it, the rainbow connection";

// Ansi is the default escape sequence
var style = new RainbowStyle();
var lolcat = new Rainbow(style);
var markup = lolcat.Markup(text);
Console.WriteLine(markup);

text = "The lovers, the dreamers and me";

// Spectre.Console escape sequence is also available
lolcat = new Rainbow(style with
{
    EscapeSequence = EscapeSequence.Spectre,
    Frequency = 1,
    Spread = 5,
    Seed = 42
});
var spectre = lolcat.Markup(text);
AnsiConsole.MarkupLine(spectre);

// Ouroboros 😎
//Console.Clear();
//lolcat = new Rainbow();
//markup = lolcat.Markup(File.ReadAllText("./Program.cs"));
//Console.WriteLine(markup);


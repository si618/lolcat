//BenchmarkRunner.Run<Benchmarks>();

// Console app with top-level statements
using Lolcat;
using Spectre.Console;

// Ansi output
var text = "Somewhere over the rainbow...";
var style = new RainbowStyle(EscapeSequence.Ansi);
var lolcat = new Rainbow(style);
var ansi = lolcat.Convert(text);
Console.Write(ansi);

// Spectre.Console output
lolcat.Style.EscapeSequence = EscapeSequence.Spectre;
var spectre = lolcat.Convert(text);
AnsiConsole.Markup(spectre);

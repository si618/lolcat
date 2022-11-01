using Lolcat;

const string text =
@"Someday we'll find it, the rainbow connection
The lovers, the dreamers and me";

var style = new RainbowStyle(
    EscapeSequence.Spectre,
    Spread: 1,
    Frequency: .5,
    Seed: 42,
    Animate: true,
    Duration: TimeSpan.FromSeconds(8),
    Speed: 2);

var lolcat = new Rainbow(style);

lolcat.Animate(args.Length == 1 ? args[0] : text);

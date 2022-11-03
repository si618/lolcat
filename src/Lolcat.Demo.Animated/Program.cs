var random = new Random();

var style = new RainbowStyle(
    EscapeSequence.Spectre,
    Spread: random.Next(1, 8),
    Frequency: random.NextDouble(),
    Seed: 42,
    Animate: true,
    Duration: TimeSpan.FromSeconds(8),
    Speed: random.Next(10, 30));

var lolcat = new Rainbow(style);

lolcat.Animate(args.Length > 0
    ? string.Join(Environment.NewLine, args)
    : DateTime.Now.Ticks % 2 == 0 ? Resources.Alien : Resources.Unicorn);

var random = new Random();

var style = new RainbowStyle(
    EscapeSequence.Spectre,
    Spread: random.Next(1, 10),
    Frequency: random.NextDouble(),
    Seed: 42,
    Duration: TimeSpan.FromSeconds(42),
    Speed: random.Next(10, 42));

var lolcat = new Rainbow(style);

lolcat.Animate(args.Length > 0
    ? string.Join(Environment.NewLine, args)
    : DateTime.Now.Ticks % 2 == 0 ? Resources.Alien : Resources.Unicorn);

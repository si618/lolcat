var random = new Random();

var rainbowStyle = new RainbowStyle(
    EscapeSequence.Spectre,
    Spread: random.Next(1, 16),
    Frequency: random.NextDouble(),
    Seed: 42);

var animationStyle = new AnimationStyle(
    Duration: TimeSpan.FromSeconds(8),
    Speed: random.Next(10, 42));

var text = args.Length > 0
    ? string.Join(Environment.NewLine, args)
    : DateTime.Now.Ticks % 2 == 0
        ? Resources.Alien
        : Resources.Unicorn;

var rainbow = Rainbow.WithStyle(rainbowStyle);

rainbow.Animate(text, animationStyle);

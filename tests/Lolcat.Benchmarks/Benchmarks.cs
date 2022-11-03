namespace Lolcat.Benchmarks;

[Config(typeof(BenchmarkConfig))]
public class Benchmarks
{
    private readonly Rainbow _ansiRainbow = new();
    private readonly Rainbow _spectreRainbow = new(new RainbowStyle(EscapeSequence.Spectre));

    [Benchmark]
    [ArgumentsSource(nameof(MarkupText))]
    public string MarkupAsAnsi(string text) => _ansiRainbow.Markup(text);

    [Benchmark]
    [ArgumentsSource(nameof(MarkupText))]
    public string MarkupAsSpectre(string text) => _spectreRainbow.Markup(text);

    public IEnumerable<object> MarkupText()
    {
        yield return AsciiSet(1);
        yield return AsciiSet(10);
        yield return AsciiSet(100);
    }

    private static string AsciiSet(int count)
    {
        var set = new StringBuilder();

        var chars = CollectionsMarshal.AsSpan(Enumerable
            .Range(65, 26)
            .SelectMany(i => new[] { i, i + 32 })
            .OrderBy(i => i)
            .Select(i => (char)i)
            .ToList());

        for (var i = 0; i < count; i++)
        {
            set.Append(chars[..chars.Length]);
            set.AppendLine();
        }

        return set.ToString();
    }
}

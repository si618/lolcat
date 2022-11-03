namespace Lolcat.Benchmarks;

[Config(typeof(BenchmarkConfig))]
public class Benchmarks
{
    private readonly Rainbow _ansiRainbow = new();
    private readonly Rainbow _spectreRainbow = new(new RainbowStyle(EscapeSequence.Spectre));

    [Benchmark]
    public string MarkupAsAnsi_Small() => _ansiRainbow.Markup(AsciiSet(1));

    [Benchmark]
    public string MarkupAsAnsi_Medium() => _ansiRainbow.Markup(AsciiSet(10));

    [Benchmark]
    public string MarkupAsAnsi_Large() => _ansiRainbow.Markup(AsciiSet(100));

    [Benchmark]
    public string MarkupAsSpectre_Small() => _spectreRainbow.Markup(AsciiSet(1));

    [Benchmark]
    public string MarkupAsSpectre_Medium() => _spectreRainbow.Markup(AsciiSet(10));

    [Benchmark]
    public string MarkupAsSpectre_Large() => _spectreRainbow.Markup(AsciiSet(100));

    private static string AsciiSet(int lineCount)
    {
        var set = new StringBuilder();

        var chars = CollectionsMarshal.AsSpan(Enumerable
            .Range(65, 26)
            .SelectMany(i => new[] { i, i + 32 })
            .OrderBy(i => i)
            .Select(i => (char)i)
            .ToList());

        for (var i = 0; i < lineCount; i++)
        {
            set.Append(chars[..chars.Length]);
            set.AppendLine();
        }

        return set.ToString();
    }
}

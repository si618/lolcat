namespace Lolcat.Benchmarks;

[Config(typeof(BenchmarkConfig))]
public class Benchmarks
{
    private readonly Rainbow _ansiRainbow = new(new RainbowStyle());
    private readonly Rainbow _spectreRainbow = new(new RainbowStyle(EscapeSequence.Spectre));

    [Benchmark]
    [ArgumentsSource(nameof(TextToConvert))]
    // ReSharper disable once InconsistentNaming
    public string ConvertToAnsi(string Text) => _ansiRainbow.Convert(Text);

    [Benchmark]
    [ArgumentsSource(nameof(TextToConvert))]
    // ReSharper disable once InconsistentNaming
    public string ConvertToSpectre(string Text) => _spectreRainbow.Convert(Text);

    public IEnumerable<object> TextToConvert()
    {
        yield return AsciiSet(1);
        yield return AsciiSet(10);
        yield return AsciiSet(100);
    }

    private static string AsciiSet(int count)
    {
        var set = new StringBuilder();

        var chars = CollectionsMarshal.AsSpan(
            Enumerable
                .Range(48, 126)
                .Select(i => (char)i)
                .ToList());

        for (var i = 0; i < count; i++)
        {
            set.Append(chars);
            set.AppendLine();
        }

        return set.ToString();
    }
}

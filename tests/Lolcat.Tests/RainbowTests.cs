namespace Lolcat.Tests;

public class RainbowTests
{
    [Fact]
    public void Convert_UsingAnsiEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: 42);
        var lolcat = new Rainbow(style);

        var converted = lolcat.Markup(Resources.AnsiRainbowText);

        converted.Should().Be(Resources.AnsiRainbowEscaped);
    }

    [Fact]
    public void Convert_UsingSpectreEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: 42);
        var lolcat = new Rainbow(style);

        var converted = lolcat.Markup(Resources.SpectreRainbowText);

        converted.Should().Be(Resources.SpectreRainbowEscaped);
    }
}

namespace Lolcat.Tests;

using FluentAssertions.Extensions;
using Moq;

public class RainbowTests
{
    [Fact]
    public void Ctor_WhenNoStyleIsPassed_CreatesDefault()
    {
        var style = new RainbowStyle();

        var lolcat = new Rainbow();

        lolcat.RainbowStyle.Should().Be(style);
    }

    [Fact]
    public void Markup_WithAnsiEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: 42);
        var lolcat = new Rainbow(style);

        var markup = lolcat.Markup(Resources.AnsiText);

        markup.Should().Be(Resources.AnsiMarkup);
    }

    [Fact]
    public void Markup_WithSpectreEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: 42);
        var lolcat = new Rainbow(style);

        var markup = lolcat.Markup(Resources.SpectreText);

        markup.Should().Be(Resources.SpectreMarkup);
    }

    [Fact]
    public void Markup_WithEmoji_JoinsSurrogatePairs()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: 42);
        var lolcat = new Rainbow(style);

        var markup = lolcat.Markup(Resources.EmojiText);

        markup.Should().Be(Resources.EmojiMarkup);
    }

    [Fact]
    public void Animate_DurationMatchesSpecifiedValue_WithinMarginOfError()
    {
        var mockConsole = new Mock<ILolcatConsole>();
        var duration = TimeSpan.FromMilliseconds(1_000);
        var style = new RainbowStyle(Animate: true, Duration: duration);
        var lolcat = new Rainbow(mockConsole.Object, style);

        var animate = () => lolcat.Animate(Resources.AnsiText);

        animate.ExecutionTime().Should().BeCloseTo(duration, 100.Milliseconds());
    }
}

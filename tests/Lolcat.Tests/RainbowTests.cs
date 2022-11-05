namespace Lolcat.Tests;

public class RainbowTests
{
    private const double Seed = 42;

    private readonly Mock<IConsole> _mockAnsiConsole = new Mock<IConsole>()
        .SetupProperty(f => f.Format, SystemConsole.AnsiFormat);
    private readonly Mock<IConsole> _mockSpectreConsole = new Mock<IConsole>()
        .SetupProperty(f => f.Format, SpectreConsole.SpectreFormat);

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
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: Seed);
        var lolcat = new Rainbow(_mockAnsiConsole.Object, style);

        var markup = lolcat.Markup(Resources.AnsiText);

        markup.Should().Be(Resources.AnsiMarkup);
    }

    [Fact]
    public void Markup_WithSpectreEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var lolcat = new Rainbow(_mockSpectreConsole.Object, style);

        var markup = lolcat.Markup(Resources.SpectreText);

        markup.Should().Be(Resources.SpectreMarkup);
    }

    [Fact]
    public void Markup_WithEmoji_JoinsSurrogatePairs()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var lolcat = new Rainbow(_mockSpectreConsole.Object, style);

        var markup = lolcat.Markup(Resources.EmojiMultilineText);

        markup.Should().Be(Resources.EmojiMultilineMarkup);
    }

    [Fact]
    public void Markup_EmptyLinesRemain_AfterInvoking()
    {
        var lolcat = new Rainbow(_mockAnsiConsole.Object, new RainbowStyle());
        var text = Environment.NewLine + Environment.NewLine;

        var markup = lolcat.Markup(text);

        markup.Should().Be(text);
    }

    [Fact]
    public void Markup_WhenPassingSeed_BuildsExpectedFrames()
    {
        var lolcat = new Rainbow(
            _mockSpectreConsole.Object,
            new RainbowStyle(EscapeSequence.Spectre, Seed: Seed));
        var seed = lolcat.RainbowStyle.Seed;

        var markupFrame1 = lolcat.Markup(Resources.SpectreFrameText, seed);
        seed += lolcat.RainbowStyle.Spread;
        var markupFrame2 = lolcat.Markup(Resources.SpectreFrameText, seed);

        markupFrame1.Should().Be(Resources.SpectreMarkupFrame1);
        markupFrame2.Should().Be(Resources.SpectreMarkupFrame2);
    }

    [Fact]
    public void MarkupLine_AddsCurrentLineTerminator_ToMarkup()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var lolcat = new Rainbow(_mockSpectreConsole.Object, style);

        var markup = lolcat.MarkupLine(Resources.EmojiMultilineText);

        markup.Should().EndWith(Environment.NewLine);
    }

    [Fact]
    public void Animate_DurationMatchesSpecifiedValue_WithinMarginOfError()
    {
        var duration = TimeSpan.FromMilliseconds(1_000);
        var style = new RainbowStyle(Duration: duration);
        var lolcat = new Rainbow(_mockAnsiConsole.Object, style);

        var animate = () => lolcat.Animate(Resources.AnsiText);

        animate.ExecutionTime().Should().BeCloseTo(duration, 100.Milliseconds());
    }

    [Fact]
    public void TrimLines_WhenWindowHeightAndWidthAreZero_DoesNothing()
    {
        var lolcat = new Rainbow(_mockSpectreConsole.Object, new RainbowStyle(Seed: Seed));

        var markup = lolcat.Markup(Resources.EmojiMultilineText);

        markup.Should().Be(Resources.EmojiMultilineMarkup);
    }

    [Fact]
    public void TrimLineHeight_WhenHeightIsHigherThanWindow_ReducesHeightToFit()
    {
        _mockSpectreConsole
            .Setup(con => con.GetWindowHeight())
            .Returns(2);
        var lolcat = new Rainbow(_mockSpectreConsole.Object, new RainbowStyle(Seed: Seed));
        var line1 = Resources.EmojiMultilineMarkup.Split(Environment.NewLine)[0];

        var markup = lolcat.Markup(Resources.EmojiMultilineText);

        markup.Should().Be(line1);
    }


    [Fact]
    public void TrimLineWidth_WhenWidthIsLargerThanWindow_ReducesWidthToFit()
    {
        _mockSpectreConsole
            .Setup(con => con.GetWindowWidth())
            .Returns(2);
        var lolcat = new Rainbow(_mockSpectreConsole.Object, new RainbowStyle(Seed: Seed));

        var markup = lolcat.Markup(Resources.EmojiMultilineText);

        markup.Should().Be(Resources.EmojiMultilineMarkupWidth1);
    }
}

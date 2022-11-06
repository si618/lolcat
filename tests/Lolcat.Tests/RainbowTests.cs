namespace Lolcat.Tests;

public class RainbowTests : TestBase
{
    [Fact]
    public void Ctor_WhenNoStyleIsPassed_CreatesDefault()
    {
        var style = new RainbowStyle();

        var rainbow = new Rainbow();

        rainbow.RainbowStyle.Should().Be(style);
    }

    [Fact]
    public void Markup_WithAnsiEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.AnsiText);

        markup.Should().Be(Resources.AnsiMarkup);
    }

    [Fact]
    public void Markup_WithSpectreEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.SpectreText);

        markup.Should().Be(Resources.SpectreMarkup.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_WithEmoji_JoinsSurrogatePairs()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.EmojiMultilineText);

        markup.Should().Be(Resources.EmojiMultilineMarkup.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_EmptyLinesRemain_AfterInvoking()
    {
        var rainbow = new Rainbow();
        var text = Environment.NewLine + Environment.NewLine;

        var markup = rainbow.Markup(text);

        markup.Should().Be(text);
    }

    [Fact]
    public void Markup_WhenPassingSeed_BuildsExpectedFrames()
    {
        var rainbow = new Rainbow(new RainbowStyle(EscapeSequence.Spectre, Seed: Seed));
        var seed = rainbow.RainbowStyle.Seed;


        var markupSeed1 = rainbow.Markup(Resources.SpectreFrameText, seed);
        seed += rainbow.RainbowStyle.Spread;
        var markupSeed2 = rainbow.Markup(Resources.SpectreFrameText, seed);

        markupSeed1.Should().Be(Resources.SpectreMarkupSeed1.ReplaceLineEndings());
        markupSeed2.Should().Be(Resources.SpectreMarkupSeed2.ReplaceLineEndings());
    }

    [Fact]
    public void MarkupLine_AddsCurrentLineTerminator_ToMarkup()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.MarkupLine(Resources.EmojiMultilineText);

        markup.Should().EndWith(Environment.NewLine);
    }
}

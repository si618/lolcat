namespace Lolcat.Tests;

public class RainbowTests : TestBase
{
    [Fact]
    public void Ctor_WhenNoStyleIsPassed_CreatesDefault()
    {
        var style = new RainbowStyle();

        var rainbow = new Rainbow();

        rainbow.RainbowStyle.ShouldBe(style);
    }

    [Fact]
    public void Markup_WithAnsiEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.AnsiText);

        markup.ShouldBe(Resources.AnsiMarkup);
    }

    [Fact]
    public void Markup_WithSpectreEscapeSequence_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.SpectreText);

        markup.ShouldBe(Resources.SpectreMarkup.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_WithSpectreEscapeSequenceOnEscapedText_BuildsExpectedResult()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.SpectreTextWithEscapeCharacters);

        markup.ShouldBe(Resources.SpectreMarkupWithEscapeCharacters.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_WithEmoji_JoinsSurrogatePairs()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.Markup(Resources.EmojiMultilineText);

        markup.ShouldBe(Resources.EmojiMultilineMarkup.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_EmptyLinesRemain_AfterInvoking()
    {
        var rainbow = new Rainbow();
        var text = Environment.NewLine + Environment.NewLine;

        var markup = rainbow.Markup(text);

        markup.ShouldBe(text);
    }

    [Fact]
    public void Markup_WhenPassingSeed_BuildsExpectedFrames()
    {
        var rainbow = new Rainbow(new RainbowStyle(EscapeSequence.Spectre, Seed: Seed));
        var seed = rainbow.RainbowStyle.Seed;


        var markupSeed1 = rainbow.Markup(Resources.SpectreFrameText, seed);
        seed += rainbow.RainbowStyle.Spread;
        var markupSeed2 = rainbow.Markup(Resources.SpectreFrameText, seed);

        markupSeed1.ShouldBe(Resources.SpectreMarkupSeed1.ReplaceLineEndings());
        markupSeed2.ShouldBe(Resources.SpectreMarkupSeed2.ReplaceLineEndings());
    }

    [Fact]
    public void MarkupLine_AddsCurrentLineTerminator_ToMarkup()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        var markup = rainbow.MarkupLine(Resources.EmojiMultilineText);

        markup.ShouldEndWith(Environment.NewLine);
    }

    [Fact]
    public void WriteLineWithMarkup_WithSpectreEscapeSequence_WorksWithEscapeCharactersInText()
    {
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        Should.NotThrow(() =>
            rainbow.WriteLineWithMarkup(Resources.SpectreTextWithEscapeCharacters));
    }
}

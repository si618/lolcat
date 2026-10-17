namespace Lolcat.Tests;

public class RainbowTests : TestBase
{
    [Fact]
    public void Ctor_NoStyle_UsesDefaultStyle()
    {
        // Arrange
        var style = new RainbowStyle();

        // Act
        var rainbow = new Rainbow();

        // Assert
        rainbow.RainbowStyle.ShouldBe(style);
    }

    [Fact]
    public void Markup_AnsiEscapeSequence_BuildsExpectedMarkup()
    {
        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Ansi, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act
        string markup = rainbow.Markup(Resources.AnsiText);

        // Assert
        markup.ShouldBe(Resources.AnsiMarkup);
    }

    [Fact]
    public void Markup_SpectreEscapeSequence_BuildsExpectedMarkup()
    {
        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act
        string markup = rainbow.Markup(Resources.SpectreText);

        // Assert
        markup.ShouldBe(Resources.SpectreMarkup.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_SpectreTextWithEscapeCharacters_BuildsExpectedMarkup()
    {
        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act
        string markup = rainbow.Markup(Resources.SpectreTextWithEscapeCharacters);

        // Assert
        markup.ShouldBe(Resources.SpectreMarkupWithEscapeCharacters.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_Emoji_JoinsSurrogatePairs()
    {
        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act
        string markup = rainbow.Markup(Resources.EmojiMultilineText);

        // Assert
        markup.ShouldBe(Resources.EmojiMultilineMarkup.ReplaceLineEndings());
    }

    [Fact]
    public void Markup_EmptyLines_KeepsLinesEmpty()
    {
        // Arrange
        var rainbow = new Rainbow();
        string text = Environment.NewLine + Environment.NewLine;

        // Act
        string markup = rainbow.Markup(text);

        // Assert
        markup.ShouldBe(text);
    }

    [Fact]
    public void Markup_SeedAdvancedBySpread_BuildsExpectedFrames()
    {
        // Arrange
        var rainbow = new Rainbow(new RainbowStyle(EscapeSequence.Spectre, Seed: Seed));
        double seed = rainbow.RainbowStyle.Seed;

        // Act
        string markupSeed1 = rainbow.Markup(Resources.SpectreFrameText, seed);
        seed += rainbow.RainbowStyle.Spread;
        string markupSeed2 = rainbow.Markup(Resources.SpectreFrameText, seed);

        // Assert
        markupSeed1.ShouldBe(Resources.SpectreMarkupSeed1.ReplaceLineEndings());
        markupSeed2.ShouldBe(Resources.SpectreMarkupSeed2.ReplaceLineEndings());
    }

    [Fact]
    public void MarkupLine_AnyText_EndsWithLineTerminator()
    {
        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act
        string markup = rainbow.MarkupLine(Resources.EmojiMultilineText);

        // Assert
        markup.ShouldEndWith(Environment.NewLine);
    }

    [Fact]
    public void WriteLineWithMarkup_SpectreTextWithEscapeCharacters_DoesNotThrow()
    {
        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act and assert
        Should.NotThrow(() =>
            rainbow.WriteLineWithMarkup(Resources.SpectreTextWithEscapeCharacters));
    }

    [Fact]
    public void Markup_SpectreMarkupCharacters_EscapesThem()
    {
        // Regression test for https://github.com/si618/lolcat/issues/152
        // StringExtensions.EscapeMarkup was moved from Spectre.Console.dll to
        // Spectre.Console.Ansi.dll in 0.55.0, causing MissingMethodException in
        // published binaries. Fix: use Markup.Escape() which stays in Spectre.Console.dll.

        // Arrange
        var style = new RainbowStyle(EscapeSequence: EscapeSequence.Spectre, Seed: Seed);
        var rainbow = new Rainbow(style);

        // Act
        string markup = rainbow.Markup("[hello]");

        // Assert
        // Output must be valid Spectre markup — i.e., [ and ] were escaped via Markup.Escape.
        // new Markup() throws if the string contains unescaped markup-special characters.
        Should.NotThrow(() => new Markup(markup));
    }
}

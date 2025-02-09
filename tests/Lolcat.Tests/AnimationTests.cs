namespace Lolcat.Tests;

public class AnimationTests : TestBase
{
    [Fact]
    public void Animate_DurationMatchesSpecifiedValue_WithinMarginOfError()
    {
        var duration = 1_000.Milliseconds();
        var style = new AnimationStyle(Duration: duration);
        var rainbow = new Rainbow(MockConsole);
        var animation = new Animation(rainbow, style);

        Should.CompleteIn(
            action: () => { animation.Animate(Resources.AnsiText); },
            timeout: duration.Add(100.Milliseconds()));
    }

    [Fact]
    public void Animate_WhenTextHeightIsGreaterThanWindowHeight_TextHeightDecreasesToFitWindowHeight()
    {
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowHeight: 1);
        var rainbow = new Rainbow(MockConsole, new RainbowStyle(Seed: Seed));
        var duration = 10.Milliseconds();
        var style = new AnimationStyle(Duration: duration);
        var animation = new Animation(rainbow, style);

        animation.Animate(Resources.EmojiMultilineText);

        animation.Rainbow.Lines.Count.ShouldBe(MockConsole.GetWindowHeight());
    }

    [Fact]
    public void Animate_WhenTextHeightIsLessThanWindowHeight_TextHeightIncreasesToFitWindowHeight()
    {
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowHeight: 10);
        var rainbow = new Rainbow(MockConsole, new RainbowStyle(Seed: Seed));
        var duration = 10.Milliseconds();
        var style = new AnimationStyle(Duration: duration);
        var animation = new Animation(rainbow, style);

        animation.Animate(Resources.EmojiMultilineText);

        animation.Rainbow.Lines.Count.ShouldBe(MockConsole.GetWindowHeight());
    }

    [Fact]
    public void Animate_WhenTextWidthIsGreaterThanWindowWidth_TextWidthDecreasesToFitWindowWidth()
    {
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowWidth: 4);
        var rainbow = new Rainbow(MockConsole, new RainbowStyle(Seed: Seed));
        var duration = 10.Milliseconds();
        var style = new AnimationStyle(Duration: duration);
        var animation = new Animation(rainbow, style);
        var windowWidth = MockConsole.GetWindowWidth();

        animation.Animate(Resources.EmojiMultilineText);

        animation.Rainbow.Lines
            .ShouldAllBe(line => line.RemoveMarkupAndPadRight(windowWidth).Length == windowWidth);
    }

    [Fact]
    public void Animate_WhenTextWidthIsLessThanWindowWidth_TextWidthIncreasesToFitWindowWidth()
    {
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowWidth: 100);
        var rainbow = new Rainbow(MockConsole, new RainbowStyle(Seed: Seed));
        var duration = TimeSpan.FromMilliseconds(10);
        var style = new AnimationStyle(Duration: duration);
        var animation = new Animation(rainbow, style);
        var windowWidth = MockConsole.GetWindowWidth();

        animation.Animate(Resources.EmojiMultilineText);

        animation.Rainbow.Lines
            .ShouldAllBe(line => line.RemoveMarkupAndPadRight(windowWidth).Length == windowWidth);
    }
}

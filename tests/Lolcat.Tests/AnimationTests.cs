namespace Lolcat.Tests;

public class AnimationTests : TestBase
{
    [Fact]
    public async Task AnimateAsync_DurationElapses_StopsAfterFrameThatReachesDuration()
    {
        // Arrange
        var time = new FakeTimeProvider(Start);
        var style = new AnimationStyle(Duration: TimeSpan.FromSeconds(1), Speed: 20);
        TimeSpan frame = FrameOf(style);
        var animation = new Animation(time, new Rainbow(MockConsole), style);
        // One frame at the start, and one after each frame interval up to and including Duration
        int frames = (int)(style.Duration!.Value / frame) + 1;

        // Act
        await AnimateToEndAsync(animation, Resources.AnsiText, time, style);

        // Assert
        time.GetUtcNow().ShouldBe(time.Start + (frames * frame));
        MockConsole.LinesWritten.ShouldBe(frames * MockConsole.GetWindowHeight());
    }

    [Fact]
    public async Task AnimateAsync_Cancelled_ThrowsAndRestoresCursorVisibility()
    {
        // Arrange
        var time = new FakeTimeProvider(Start);
        var animation = new Animation(time, new Rainbow(MockConsole), new AnimationStyle());
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(
            TestContext.Current.CancellationToken);
        Task animate = animation.AnimateAsync(Resources.AnsiText, cancellation.Token);

        // Act
        await cancellation.CancelAsync();

        // Assert: the real-time limit only stops a hang if the token isn't honoured
        await Should.ThrowAsync<TaskCanceledException>(animate.WaitAsync(
            TimeSpan.FromSeconds(5), TimeProvider.System, TestContext.Current.CancellationToken));
        MockConsole.GetCursorVisibility().ShouldBeTrue();
    }

    [Fact]
    public async Task AnimateAsync_TextTallerThanWindow_TrimsLinesToWindowHeight()
    {
        // Arrange
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowHeight: 1);

        // Act
        Animation animation = await AnimateOneFrameAsync();

        // Assert
        animation.Rainbow.Lines.Count.ShouldBe(MockConsole.GetWindowHeight());
    }

    [Fact]
    public async Task AnimateAsync_TextShorterThanWindow_PadsLinesToWindowHeight()
    {
        // Arrange
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowHeight: 10);

        // Act
        Animation animation = await AnimateOneFrameAsync();

        // Assert
        animation.Rainbow.Lines.Count.ShouldBe(MockConsole.GetWindowHeight());
    }

    [Fact]
    public async Task AnimateAsync_TextWiderThanWindow_TrimsLinesToWindowWidth()
    {
        // Arrange
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowWidth: 4);
        int windowWidth = MockConsole.GetWindowWidth();

        // Act
        Animation animation = await AnimateOneFrameAsync();

        // Assert
        animation.Rainbow.Lines
            .ShouldAllBe(line => line.RemoveMarkupAndPadRight(windowWidth).Length == windowWidth);
    }

    [Fact]
    public async Task AnimateAsync_TextNarrowerThanWindow_PadsLinesToWindowWidth()
    {
        // Arrange
        MockConsole = new MockConsole(EscapeSequence.Spectre, windowWidth: 100);
        int windowWidth = MockConsole.GetWindowWidth();

        // Act
        Animation animation = await AnimateOneFrameAsync();

        // Assert
        animation.Rainbow.Lines
            .ShouldAllBe(line => line.RemoveMarkupAndPadRight(windowWidth).Length == windowWidth);
    }

    /// <summary>
    /// Animates multiline emoji text on <see cref="TestBase.MockConsole"/> for a duration
    /// shorter than one frame, so exactly one frame is drawn
    /// </summary>
    private async Task<Animation> AnimateOneFrameAsync()
    {
        var time = new FakeTimeProvider(Start);
        var style = new AnimationStyle(Duration: TimeSpan.FromMilliseconds(10));
        var rainbow = new Rainbow(MockConsole, new RainbowStyle(Seed: Seed));
        var animation = new Animation(time, rainbow, style);

        await AnimateToEndAsync(animation, Resources.EmojiMultilineText, time, style)
            .ConfigureAwait(false);

        return animation;
    }
}

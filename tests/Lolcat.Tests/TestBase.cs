namespace Lolcat.Tests;

public abstract class TestBase
{
    protected const double Seed = 42;

    /// <summary>
    /// The one start instant for every <see cref="FakeTimeProvider"/> in the suite
    /// </summary>
    protected static readonly DateTimeOffset Start = new(2026, 10, 17, 0, 0, 0, TimeSpan.Zero);

    protected MockConsole MockConsole { get; set; } = new();

    /// <summary>
    /// The time between frames at <paramref name="style"/>'s speed
    /// </summary>
    protected static TimeSpan FrameOf(AnimationStyle style) => TimeSpan.FromSeconds(1) / style.Speed;

    /// <summary>
    /// Runs <paramref name="animation"/> to completion, advancing fake time one frame at a time,
    /// and fails if it's still running one frame after <paramref name="style"/>'s duration
    /// </summary>
    protected static async Task AnimateToEndAsync(
        Animation animation, string text, FakeTimeProvider time, AnimationStyle style)
    {
        TimeSpan frame = FrameOf(style);
        DateTimeOffset deadline = time.GetUtcNow() + style.Duration!.Value + frame;
        Task animate = animation.AnimateAsync(text, TestContext.Current.CancellationToken);

        while (!animate.IsCompleted && time.GetUtcNow() < deadline)
        {
            time.Advance(frame);
        }

        animate.IsCompleted.ShouldBeTrue("the animation should stop once its duration elapses");
        await animate.ConfigureAwait(false);
    }
}

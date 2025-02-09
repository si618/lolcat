namespace Lolcat.Tests;

public class AnimationStyleTests : TestBase
{
    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpeedIsZero()
    {
        var ctor = () => new AnimationStyle(Speed: 0);
        var message = $"Less than 1 (Parameter 'Speed'){NL}Actual value was 0.{NL}";

        ctor.ShouldThrow<ArgumentOutOfRangeException>(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpeedIsLessThanOne()
    {
        var ctor = () => new AnimationStyle(Speed: -1);
        var message = $"Less than 1 (Parameter 'Speed'){NL}Actual value was -1.{NL}";

        ctor.ShouldThrow<ArgumentOutOfRangeException>(message);
    }

    [Fact]
    public void Ctor_SetsDurationToTwelveSeconds_WhenNoDurationSpecified()
    {
        var style = new AnimationStyle();

        style.Duration.ShouldBe(TimeSpan.FromSeconds(12));
    }

    [Fact]
    public void Ctor_SetsSpeedToTwenty_WhenNoSpeedSpecified()
    {
        var style = new AnimationStyle();

        style.Speed.ShouldBe(20);
    }
}

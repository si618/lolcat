namespace Lolcat.Tests;

public class AnimationStyleTests : TestBase
{
    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpeedIsZero()
    {
        var ctor = () => new AnimationStyle(Speed: 0D);
        var message = $"Zero or less (Parameter 'Speed'){NL}Actual value was 0.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpeedIsLessThanZero()
    {
        var ctor = () => new AnimationStyle(Speed: -1D);
        var message = $"Zero or less (Parameter 'Speed'){NL}Actual value was -1.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_SetsDurationToTwelveSeconds_WhenNoDurationSpecified()
    {
        var style = new AnimationStyle();

        style.Duration.Should().Be(TimeSpan.FromSeconds(12));
    }

    [Fact]
    public void Ctor_SetsSpeedToTwenty_WhenNoSpeedSpecified()
    {
        var style = new AnimationStyle();

        style.Speed.Should().Be(20);
    }
}

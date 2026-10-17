namespace Lolcat.Tests;

public class AnimationStyleTests : TestBase
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_SpeedLessThanOne_ThrowsArgumentOutOfRangeException(int speed)
    {
        // Act
        ArgumentOutOfRangeException exception = Should.Throw<ArgumentOutOfRangeException>(
            () => new AnimationStyle(Speed: speed));

        // Assert
        exception.ParamName.ShouldBe(nameof(AnimationStyle.Speed));
        exception.ActualValue.ShouldBe(speed);
        exception.Message.ShouldStartWith("Less than 1");
    }

    [Fact]
    public void Ctor_NoDuration_DefaultsToTwelveSeconds()
    {
        // Arrange and act
        var style = new AnimationStyle();

        // Assert
        style.Duration.ShouldBe(TimeSpan.FromSeconds(12));
    }

    [Fact]
    public void Ctor_NoSpeed_DefaultsToTwenty()
    {
        // Arrange and act
        var style = new AnimationStyle();

        // Assert
        style.Speed.ShouldBe(20);
    }
}

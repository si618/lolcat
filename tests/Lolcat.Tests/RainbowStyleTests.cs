namespace Lolcat.Tests;

public class RainbowStyleTests : TestBase
{
    [Theory]
    [InlineData(1.001, "Greater than max '1'")]
    [InlineData(0, "Less than min '0.001'")]
    public void Ctor_FrequencyOutOfRange_ThrowsArgumentOutOfRangeException(
        double frequency, string message)
    {
        // Act
        ArgumentOutOfRangeException exception = Should.Throw<ArgumentOutOfRangeException>(
            () => new RainbowStyle(Frequency: frequency));

        // Assert
        exception.ParamName.ShouldBe(nameof(RainbowStyle.Frequency));
        exception.ActualValue.ShouldBe(frequency);
        exception.Message.ShouldStartWith(message);
    }

    [Theory]
    [InlineData(1_000, "Greater than max '100'")]
    [InlineData(0, "Less than min '1'")]
    public void Ctor_SpreadOutOfRange_ThrowsArgumentOutOfRangeException(
        double spread, string message)
    {
        // Act
        ArgumentOutOfRangeException exception = Should.Throw<ArgumentOutOfRangeException>(
            () => new RainbowStyle(Spread: spread));

        // Assert
        exception.ParamName.ShouldBe(nameof(RainbowStyle.Spread));
        exception.ActualValue.ShouldBe(spread);
        exception.Message.ShouldStartWith(message);
    }

    [Fact]
    public void With_SpreadOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var style = new RainbowStyle();

        // Act
        ArgumentOutOfRangeException exception = Should.Throw<ArgumentOutOfRangeException>(() => style with { Spread = 500 });

        // Assert
        exception.ParamName.ShouldBe(nameof(RainbowStyle.Spread));
        exception.ActualValue.ShouldBe(500d);
    }

    [Fact]
    public void ObjectInitializer_FrequencyOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Act
        ArgumentOutOfRangeException exception = Should.Throw<ArgumentOutOfRangeException>(
            () => new RainbowStyle { Frequency = -1 });

        // Assert
        exception.ParamName.ShouldBe(nameof(RainbowStyle.Frequency));
        exception.ActualValue.ShouldBe(-1d);
    }

    [Fact]
    public void With_SpreadInRange_CopiesStyleWithNewSpread()
    {
        // Arrange
        var style = new RainbowStyle(Frequency: 0.5, Seed: Seed);

        // Act
        RainbowStyle copy = style with { Spread = 50 };

        // Assert
        copy.ShouldBe(new RainbowStyle(Spread: 50, Frequency: 0.5, Seed: Seed));
    }
}

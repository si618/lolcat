namespace Lolcat.Tests;

public class RainbowStyleTests : TestBase
{
    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenFrequencyTooHigh()
    {
        Func<RainbowStyle> ctor = () => new RainbowStyle(Frequency: 1.001);
        string message = $"Greater than max '1' (Parameter 'Frequency'){NL}Actual value was 1.001.{NL}";

        ctor.ShouldThrow<ArgumentOutOfRangeException>(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenFrequencyTooLow()
    {
        Func<RainbowStyle> ctor = () => new RainbowStyle(Frequency: 0);
        string message = $"Less than min '0.001' (Parameter 'Frequency'){NL}Actual value was 0.{NL}";

        ctor.ShouldThrow<ArgumentOutOfRangeException>(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpreadTooHigh()
    {
        Func<RainbowStyle> ctor = () => new RainbowStyle(Spread: 1_000);
        string message = $"Greater than max '100' (Parameter 'Spread'){NL}Actual value was 1000.{NL}";

        ctor.ShouldThrow<ArgumentOutOfRangeException>(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpreadTooLow()
    {
        Func<RainbowStyle> ctor = () => new RainbowStyle(Spread: 0);
        string message = $"Less than min '1' (Parameter 'Spread'){NL}Actual value was 0.{NL}";

        ctor.ShouldThrow<ArgumentOutOfRangeException>(message);
    }
}

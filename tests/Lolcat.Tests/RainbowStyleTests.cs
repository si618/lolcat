namespace Lolcat.Tests;

public class RainbowStyleTests
{
    // ReSharper disable once InconsistentNaming
    private static readonly string NL = Environment.NewLine;

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenFrequencyTooHigh()
    {
        var ctor = () => new RainbowStyle(Frequency: 1.001);
        var message = $"Greater than max '1' (Parameter 'Frequency'){NL}Actual value was 1.001.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenFrequencyTooLow()
    {
        var ctor = () => new RainbowStyle(Frequency: 0);
        var message = $"Less than min '0.001' (Parameter 'Frequency'){NL}Actual value was 0.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpreadTooHigh()
    {
        var ctor = () => new RainbowStyle(Spread: 1_000);
        var message = $"Greater than max '100' (Parameter 'Spread'){NL}Actual value was 1000.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpreadTooLow()
    {
        var ctor = () => new RainbowStyle(Spread: 0);
        var message = $"Less than min '1' (Parameter 'Spread'){NL}Actual value was 0.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpeedTooLow()
    {
        var ctor = () => new RainbowStyle(Speed: 0);
        var message = $"Less than min '{0.1D}' (Parameter 'Speed ?? 20'){NL}Actual value was {0D}.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }

    [Fact]
    public void Ctor_ThrowsArgumentOutOfRangeException_WhenSpeedTooHigh()
    {
        var ctor = () => new RainbowStyle(Speed: 101);
        var message = $"Greater than max '{100}' (Parameter 'Speed ?? 20'){NL}Actual value was {101M}.{NL}";

        ctor.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(message);
    }
}

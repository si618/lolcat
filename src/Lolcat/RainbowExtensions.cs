namespace Lolcat;

public static class RainbowExtensions
{
    /// <summary>
    /// Animate <paramref name="text" /> as a rainbow using <see cref="Lolcat.AnimationStyle"/> and
    /// <see cref="RainbowStyle"/>
    /// </summary>
    /// <param name="rainbow">The rainbow to animate</param>
    /// <param name="text">The text to animate</param>
    /// <param name="style">The animation style</param>
    /// <remarks>
    /// Console window resizing is not fully supported, expect strange things to happen :)
    /// </remarks>
    /// <returns>Instance of an <see cref="Animation"/> object</returns>
    public static void Animate(this Rainbow rainbow, string text, AnimationStyle? style = null)
    {
        var animation = new Animation(rainbow, style);

        animation.Animate(text);
    }
}
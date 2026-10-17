using System.ComponentModel;

namespace Lolcat;

/// <summary>
/// Binary compatibility with 1.1, which shipped <see cref="Rainbow.Animate"/> as an extension method.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class RainbowExtensions
{
    extension(Rainbow rainbow)
    {
        /// <summary>
        /// Animate <paramref name="text" /> as a rainbow. Use <see cref="Rainbow.Animate"/>.
        /// </summary>
        /// <param name="text">The text to animate</param>
        /// <param name="style">The animation style</param>
        [Obsolete("Use the Rainbow.Animate instance method.")]
        public void Animate(string text, AnimationStyle? style = null) =>
            rainbow.Animate(text, style);
    }
}

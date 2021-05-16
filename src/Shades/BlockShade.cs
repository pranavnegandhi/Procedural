using SkiaSharp;

namespace Notadesigner.Shades
{
    /// <summary>
    /// Type of shade that will always fill with defined color without variation.
    /// </summary>
    public class BlockShade : Shade
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">The colour value to apply for this shade.</param>
        /// <param name="warpSize">How much warp noise is allowed to alter the mark in pixels.</param>
        public BlockShade(SKColor color, int warpSize = 0) : base(warpSize)
        {
            Color = color;
        }

        public SKColor Color
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override SKColor DetermineShade(SKPoint point)
        {
            return Color;
        }
    }
}
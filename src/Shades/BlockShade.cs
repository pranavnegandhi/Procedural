using SkiaSharp;

namespace Notadesigner.Shades
{
    public class BlockShade : Shade
    {
        public BlockShade(SKColor color, int warpSize = 0) : base(color, warpSize)
        {
        }

        public override SKColor DetermineShade(SKPoint point)
        {
            return Color;
        }
    }
}
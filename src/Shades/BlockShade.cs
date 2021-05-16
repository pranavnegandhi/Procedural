using SkiaSharp;

namespace Notadesigner.Shades
{
    public class BlockShade : Shade
    {
        public BlockShade(SKColor color, int warpSize = 0) : base(warpSize)
        {
            Color = color;
        }

        public SKColor Color
        {
            get;
            set;
        }

        public override SKColor DetermineShade(SKPoint point)
        {
            return Color;
        }
    }
}
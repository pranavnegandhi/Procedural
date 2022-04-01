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

        /// <inheritdoc/>
        public override void Line(SKBitmap canvas, SKPoint point1, SKPoint point2, int weight = 2)
        {
            if ((WarpNoises[0].Scale > 0 || WarpNoises[1].Scale > 0) && WarpSize > 0)
            {
                base.Line(canvas, point1, point2, weight);
            }

            var fill = new SKCanvas(canvas);
            var paint = new SKPaint()
            {
                Color = Color,
                StrokeWidth = weight
            };
            var offset = weight >> 1;
            fill.DrawLine(point1.X - offset, point1.Y - offset, point2.X - offset, point2.Y - offset, paint);
        }

        /// <inheritdoc/>
        public override void Fill(SKBitmap canvas)
        {
            if ((WarpNoises[0].Scale > 0 || WarpNoises[1].Scale > 0) && WarpSize > 0)
            {
                base.Fill(canvas);
            }

            var fill = new SKCanvas(canvas);
            var paint = new SKPaint()
            {
                Color = Color
            };
            fill.DrawRect(0, 0, canvas.Width, canvas.Height, paint);
        }
    }
}
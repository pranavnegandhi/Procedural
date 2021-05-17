using SkiaSharp;

namespace Notadesigner.Shades
{
    /// <summary>
    /// Type of shade that will produce varying gradient based on noise fields.
    /// </summary>
    public class NoiseGradient : Shade
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">Central RGB color of shade. Defaults to black.</param>
        /// <param name="warpSize">How much warp_noise is allowed to alter the mark in pixels. Defaults to 0.</param>
        /// <param name="colorVariance">How much noise is allowed to affect the color from the central shade.</param>
        public NoiseGradient(SKColor color, int warpSize = 0, int colorVariance = 70) : base(warpSize)
        {
            Color = color;
            ColorVariance = colorVariance;
            NoiseFields = new NoiseField[]
            {
                new NoiseField(),
                new NoiseField(),
                new NoiseField(),
                new NoiseField()
            };
        }

        public SKColor Color
        {
            get;
            set;
        }

        public int ColorVariance
        {
            get;
            set;
        }

        public NoiseField[] NoiseFields
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override SKColor DetermineShade(SKPoint point)
        {
            var noise = NoiseFields[0].Noise(point) - 0.5;
            var colorAffect = noise * (2 * ColorVariance);
            var red = Color.Red + colorAffect;

            noise = NoiseFields[1].Noise(point) - 0.5;
            colorAffect = noise * (2 * ColorVariance);
            var green = Color.Green + colorAffect;

            noise = NoiseFields[2].Noise(point) - 0.5;
            colorAffect = noise * (2 * ColorVariance);
            var blue = Color.Blue + colorAffect;

            var alpha = Color.Alpha;

            return ColorUtilities.ColorClamp(red, green, blue, alpha);
        }
    }
}
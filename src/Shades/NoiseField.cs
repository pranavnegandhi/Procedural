using SkiaSharp;
using System;

namespace Notadesigner.Shades
{
    public class NoiseField
    {
        public NoiseField(float scale = 0.02f, long? seed = null)
        {
            Scale = scale;
            seed ??= (new Random((int)DateTime.Now.Ticks).Next());
            Simplex = new OpenSimplexNoise((long)seed);
        }

        public float Scale
        {
            get;
            set;
        }

        public OpenSimplexNoise Simplex
        {
            get;
            set;
        }

        /// <summary>
        /// Return the simplex noise of 2d coordinates.
        /// </summary>
        /// <param name="point">Point to use to generate the noise from.</param>
        /// <returns>A noise value between 0 and 1 from the given coordinates</returns>
        public double Noise(SKPoint point)
        {
            var noise = Simplex.Evaluate((double)point.X * Scale, (double)point.Y * Scale) + 1;
            noise /= 2;

            return noise;
        }

        /// <summary>
        /// Returns domain warped recursive simplex noise (number between 0 and 1) from 2D coordinates.
        /// </summary>
        /// <param name="point">Point to use to generate the noise from.</param>
        /// <param name="depth">Number of times the recursive call is to be made. Defaults to 1.</param>
        /// <param name="feedback">Size of warping affect of recursive noise, for normal effects use 0-1 ranges. Defaults to 0.7.</param>
        /// <returns>A noise value between 0 and 1 from the given coordinates</returns>
        public double RecursiveNoise(SKPoint point, int depth = 1, double feedback = 0.7)
        {
            if (depth <= 0)
            {
                return Noise(point);
            }
            else
            {
                var x = (float)(point.X * Scale + RecursiveNoise(point, depth - 1, feedback) * (feedback * 300));
                var y = (float)(point.Y * Scale + RecursiveNoise(point, depth - 1, feedback) * (feedback * 300));
                var newPoint = new SKPoint(x, y);

                return Noise(newPoint);
            }
        }
    }
}
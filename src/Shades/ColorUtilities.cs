using SkiaSharp;
using System;

namespace Notadesigner.Shades
{
    public static class ColorUtilities
    {
        /// <summary>
        /// Ensures a three part iterable is a properly formatted color.
        /// </summary>
        /// <param name="red">Value of the red component.</param>
        /// <param name="green">Value of the green component.</param>
        /// <param name="blue">Value of the blue component.</param>
        /// <param name="alpha">Value of the alpha component.</param>
        /// <returns>Color object made up of the input components clamped between 0 to 255.</returns>
        public static SKColor ColorClamp(int red, int green, int blue, int alpha)
        {
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));
            alpha = Math.Max(0, Math.Min(255, alpha));

            return new SKColor((byte)red, (byte)green, (byte)blue, (byte)alpha);
        }

        /// <summary>
        /// Ensures a four part iterable is a properly formatted color.
        /// </summary>
        /// <param name="red">Value of the red component.</param>
        /// <param name="green">Value of the green component.</param>
        /// <param name="blue">Value of the blue component.</param>
        /// <param name="alpha">Value of the alpha component.</param>
        /// <returns>Color object made up of the input components clamped between 0 to 255.</returns>
        public static SKColor ColorClamp(float red, float green, float blue, float alpha)
        {
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));
            alpha = Math.Max(0, Math.Min(255, alpha));

            return new SKColor((byte)red, (byte)green, (byte)blue, (byte)alpha);
        }

        /// <summary>
        /// Ensures a four part iterable is a properly formatted color.
        /// </summary>
        /// <param name="red">Value of the red component.</param>
        /// <param name="green">Value of the green component.</param>
        /// <param name="blue">Value of the blue component.</param>
        /// <param name="alpha">Value of the alpha component.</param>
        /// <returns>Color object made up of the input components clamped between 0 to 255.</returns>
        public static SKColor ColorClamp(double red, double green, double blue, double alpha)
        {
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));
            alpha = Math.Max(0, Math.Min(255, alpha));

            return new SKColor((byte)red, (byte)green, (byte)blue, (byte)alpha);
        }
    }
}
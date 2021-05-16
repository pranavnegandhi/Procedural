using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notadesigner.Shades
{
    /// <summary>
    /// Type of shade that will determine color based on transition between various colour points.
    /// </summary>
    public class LinearGradient : Shade
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="colorPoints">A map of coordinates and colours to transition betweens.</param>
        /// <param name="axis">Determines the direction of the colour transitions.</param>
        /// <param name="warpSize">How much warp noise is allowed to alter the mark in pixels.</param>
        public LinearGradient(IReadOnlyDictionary<int, SKColor> colorPoints, GradientAxis axis, int warpSize = 0) : base(warpSize)
        {
            ColorPoints = colorPoints;
            Axis = axis;
        }

        public IReadOnlyDictionary<int, SKColor> ColorPoints
        {
            get;
            set;
        }

        public GradientAxis Axis
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override SKColor DetermineShade(SKPoint point)
        {
            var other = point.X;
            if (Axis == GradientAxis.Vertical)
            {
                other = point.Y;
            }

            var larger = ColorPoints
                .Where(i => i.Key >= other)
                .Select(i => i.Key)
                .ToList();
            var smaller = ColorPoints
                .Where(i => i.Key < other)
                .Select(i => i.Key)
                .ToList();

            int next, last;
            SKColor nextColor, lastColor;

            if (smaller.Count == 0)
            {
                next = larger.Min();
                nextColor = ColorPoints[next];

                return nextColor;
            }
            else if (larger.Count == 0)
            {
                last = smaller.Min();
                lastColor = ColorPoints[last];

                return lastColor;
            }

            next = larger.Min();
            last = smaller.Max();

            nextColor = ColorPoints[next];
            lastColor = ColorPoints[last];

            var distanceFromNext = Math.Abs(next - other);
            var distanceFromLast = Math.Abs(last - other);
            var fromLastToNext = distanceFromLast / (distanceFromNext + distanceFromLast);

            var difference = (lastColor.Red - nextColor.Red) * fromLastToNext;
            var red = Convert.ToByte(lastColor.Red - difference);
            difference = (lastColor.Green - nextColor.Green) * fromLastToNext;
            var green = Convert.ToByte(lastColor.Green - difference);
            difference = (lastColor.Blue - nextColor.Blue) * fromLastToNext;
            var blue = Convert.ToByte(lastColor.Blue - difference);
            difference = (lastColor.Alpha - nextColor.Alpha) * fromLastToNext;
            var alpha = Convert.ToByte(lastColor.Alpha - difference);

            return new SKColor(red, green, blue, alpha);
        }
    }
}
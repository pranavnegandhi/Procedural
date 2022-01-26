using SkiaSharp;
using System;

namespace Notadesigner.Shades
{
    public static class GeometryUtilities
    {
        /// <summary>
        /// Returns the Euclidean distance between two points.
        /// </summary>
        /// <param name="point1">First point.</param>
        /// <param name="point2">Second point.</param>
        /// <returns>Euclidean distance betewen the two points.</returns>
        public static double DistanceBetweenPoints(SKPoint point1, SKPoint point2) => Math.Sqrt(
            ((point1.X - point2.X) * (point1.X - point2.X)) +
            ((point1.Y - point2.Y) * (point1.Y - point2.Y)));

        /// <summary>
        /// Returns the Euclidean distance between two points as an integer.
        /// </summary>
        /// <param name="point1">First point.</param>
        /// <param name="point2">Second point.</param>
        /// <returns>Euclidean distance betewen the two points rounded to the nearest integer.</returns>
        public static int DistanceBetweenPoints(SKPointI point1, SKPointI point2)
        {
            var distance = Math.Sqrt(
               ((point1.X - point2.X) * (point1.X - point2.X)) +
               ((point1.Y - point2.Y) * (point1.Y - point2.Y)));

            return Convert.ToInt32(distance);
        }
    }
}
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades
{
    /// <summary>
    /// A collection of extension methods to adapt the Shades API towards
    /// more idiomatic C# syntax. These methods enable using the method
    /// against the <see cref="SKBitmap"/> instance itself and passing in
    /// the <see cref="Shade"/> instance as a parameter. This is similar to
    /// how it's done in other C# APIs such as GDI+, where the bitmap is
    /// the target of the operation and the brush is a parameter.
    ///
    /// Some examples:
    /// <code>
    /// canvas.Point(shade, location);
    /// canvas.Circle(shade, origin, radius);
    /// </code>
    ///
    /// See the respective method documentation for usage guidelines.
    /// </summary>
    public static class SKBitmapExtensions
    {
        public static SKColor ApplyTransparency(this SKBitmap canvas, Shade shade, SKPoint point, SKColor color)
        {
            return shade.ApplyTransparency(point, canvas, color);
        }

        public static void Point(this SKBitmap canvas, Shade shade, SKPoint point)
        {
            shade.Point(canvas, point);
        }

        public static void WeightedPoint(this SKBitmap canvas, Shade shade, SKPoint point, int weight)
        {
            shade.WeightedPoint(canvas, point, weight);
        }

        public static void Line(this SKBitmap canvas, Shade shade, SKPoint point1, SKPoint point2, int weight = 2)
        {
            shade.Line(canvas, point1, point2, weight);
        }

        public static void Fill(this SKBitmap canvas, Shade shade)
        {
            shade.Fill(canvas);
        }

        public static void Shape(this SKBitmap canvas, Shade shade, IList<SKPoint> points)
        {
            shade.Shape(canvas, points);
        }

        public static void ShapeOutline(this SKBitmap canvas, Shade shade, IList<SKPoint> points, int weight = 1)
        {
            shade.ShapeOutline(canvas, points, weight);
        }

        public static void Rectangle(this SKBitmap canvas, Shade shade, SKPoint origin, float width, float height)
        {
            shade.Rectangle(canvas, origin, width, height);
        }

        public static void Triangle(this SKBitmap canvas, Shade shade, SKPoint point1, SKPoint point2, SKPoint point3)
        {
            shade.Triangle(canvas, point1, point2, point3);
        }

        public static void TriangleOutline(this SKBitmap canvas, Shade shade, SKPoint point1, SKPoint point2, SKPoint point3, int weight = 1)
        {
            shade.TriangleOutline(canvas, point1, point2, point3, weight);
        }

        public static void Circle(this SKBitmap canvas, Shade shade, SKPoint origin, float radius)
        {
            shade.Circle(canvas, origin, radius);
        }

        public static void CircleOutline(this SKBitmap canvas, Shade shade, SKPoint origin, float radius, int weight = 1)
        {
            shade.CircleOutline(canvas, origin, radius, weight);
        }
    }
}
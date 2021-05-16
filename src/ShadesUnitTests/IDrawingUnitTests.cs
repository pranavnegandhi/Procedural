using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    public interface IDrawingUnitTests
    {
        void CircleOutlineTest();

        void CircleTest();

        void FillTest();

        void LineTest();

        void PointTest();

        void RectangleTest();

        void ShapeOutlineTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex);

        void ShapeTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex);

        void TriangleOutlineTest();

        void TriangleTest();

        void WeightedPointTest();
    }
}
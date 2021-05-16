using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    public class TestDataSources
    {
        public static IEnumerable<TestCaseData> ApplyTransparencyData()
        {
            var bitmap = new SKBitmap(1, 1, false);
            bitmap.SetPixel(0, 0, new SKColor(255, 0, 0));
            var applied = new SKColor(0, 0, 255);
            var expected = new SKColor(204, 0, 51);
            yield return new TestCaseData(bitmap, applied).Returns(expected);
        }

        public static IEnumerable<TestCaseData> AdjustPointData()
        {
            var point = new SKPoint(100, 100);
            var seed = 0;

            var warpSize = 0;
            var scale = 0;
            var expected = new SKPoint(100, 100);
            yield return new TestCaseData(point, warpSize, scale, seed, expected);

            warpSize = 1;
            scale = 0;
            expected = new SKPoint(100.5f, 100.5f);
            yield return new TestCaseData(point, warpSize, scale, seed, expected);

            warpSize = 1;
            scale = 10;
            expected = new SKPoint(100.59f, 100.59f);
            yield return new TestCaseData(point, warpSize, scale, seed, expected);
        }

        public static IEnumerable<TestCaseData> GetShapeEdgeData()
        {
            var edgePoints = new List<SKPoint>()
            {
                new SKPoint(80, 20),
                new SKPoint(30, 80),
                new SKPoint(10, 60),
                new SKPoint(0, 0)
            };

            var expected = new Queue<SKPoint>();
            expected.Enqueue(new SKPoint(80, 20));
            expected.Enqueue(new SKPoint(30, 80));
            expected.Enqueue(new SKPoint(10, 60));
            expected.Enqueue(new SKPoint(0, 0));

            var testIndex = 0;

            yield return new TestCaseData(edgePoints, expected, testIndex++);

            edgePoints = new List<SKPoint>()
            {
                new SKPoint(50, 0),
                new SKPoint(75, 25),
                new SKPoint(75, 75),
                new SKPoint(25, 75),
                new SKPoint(25, 25)
            };

            expected = new Queue<SKPoint>();
            expected.Enqueue(new SKPoint(50, 0));
            expected.Enqueue(new SKPoint(75, 25));
            expected.Enqueue(new SKPoint(75, 75));
            expected.Enqueue(new SKPoint(25, 75));
            expected.Enqueue(new SKPoint(25, 25));

            yield return new TestCaseData(edgePoints, expected, testIndex++);
        }
    }
}
using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    [TestFixture]
    public class ShadeTests
    {
        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.ApplyTransparencyData))]
        public SKColor ApplyTransparencyTest(SKBitmap canvas, SKColor applied)
        {
            var instance = new BlockShade(new SKColor(0, 0, 0, 204));
            var result = instance.ApplyTransparency(new SKPointI(0, 0), canvas, applied);

            return result;
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.AdjustPointData))]
        public void AdjustPointTest(SKPoint point, int warpSize, float scale, long seed, SKPoint expected)
        {
            var instance = new BlockShade(new SKColor(0, 0, 0, 255), warpSize);
            instance.WarpNoises[0] = new NoiseField(scale, seed);
            instance.WarpNoises[1] = new NoiseField(scale, seed);

            var result = instance.AdjustPoint(point);

            Assert.That(result.X, Is.EqualTo(expected.X).Within(0.1));
            Assert.That(result.Y, Is.EqualTo(expected.Y).Within(0.1));
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        public void GetShapeEdgeTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var instance = new BlockShade(new SKColor(255, 0, 0, 127), 0);

            var result = instance.GetShapeEdge(edgePoints);

            foreach (var point in result)
            {
                var p = expected.Peek();
                if (p.X == point.X && p.Y == point.Y)
                {
                    p = expected.Dequeue();
                }
            }

            Assert.AreEqual(0, expected.Count);
        }
    }
}
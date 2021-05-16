using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    [TestFixture]
    public class ShadeTests
    {
        private static IEnumerable<TestCaseData> ApplyTransparencyData()
        {
            var bitmap = new SKBitmap(1, 1, false);
            bitmap.SetPixel(0, 0, new SKColor(255, 0, 0));
            var applied = new SKColor(0, 0, 255);
            var expected = new SKColor(204, 0, 51);
            yield return new TestCaseData(bitmap, applied).Returns(expected);
        }

        [TestCaseSource(typeof(ShadeTests), nameof(ApplyTransparencyData))]
        public SKColor ApplyTransparencyTest(SKBitmap canvas, SKColor applied)
        {
            var instance = new BlockShade(new SKColor(0, 0, 0, 204));
            var result = instance.ApplyTransparency(new SKPointI(0, 0), canvas, applied);

            return result;
        }

        private static IEnumerable<TestCaseData> AdjustPointData()
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

        [TestCaseSource(typeof(ShadeTests), nameof(AdjustPointData))]
        public void AdjustPointTest(SKPoint point, int warpSize, float scale, long seed, SKPoint expected)
        {
            var instance = new BlockShade(new SKColor(0, 0, 0, 255), warpSize);
            instance.WarpNoises[0] = new NoiseField(scale, seed);
            instance.WarpNoises[1] = new NoiseField(scale, seed);

            var result = instance.AdjustPoint(point);

            Assert.That(result.X, Is.EqualTo(expected.X).Within(0.1));
            Assert.That(result.Y, Is.EqualTo(expected.Y).Within(0.1));
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void PointTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(255, 0, 0, 16));
            instance.Point(canvas, new SKPoint(50, 50));

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(PointTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void WeightedPointTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(255, 0, 0, 51));
            instance.WeightedPoint(canvas, new SKPoint(50, 50), 5);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(WeightedPointTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void LineTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(255, 0, 0, 16));
            instance.Line(canvas, new SKPoint(0, 0), new SKPoint(100, 100), 2);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(LineTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void FillTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Fill(canvas);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(FillTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        private static IEnumerable<TestCaseData> GetShapeEdgeData()
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

        [TestCaseSource(typeof(ShadeTests), nameof(GetShapeEdgeData))]
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

        [TestCaseSource(typeof(ShadeTests), nameof(GetShapeEdgeData))]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void ShapeTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Shape(canvas, edgePoints);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(ShapeTest)}{testIndex:00}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCaseSource(typeof(ShadeTests), nameof(GetShapeEdgeData))]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void ShapeOutlineTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.ShapeOutline(canvas, edgePoints);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(ShapeOutlineTest)}{testIndex:00}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void RectangleTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Rectangle(canvas, new SKPoint(25, 25), 50, 50);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(RectangleTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Triangle(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(TriangleTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleOutlineTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.TriangleOutline(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(TriangleOutlineTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(255, 0, 0, 16));
            instance.Circle(canvas, new SKPoint(50, 50), 45.0f);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(CircleTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleOutlineTest()
        {
            var canvas = new SKBitmap(100, 100, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.CircleOutline(canvas, new SKPoint(50, 50), 40.0f);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"output-{nameof(CircleOutlineTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }
    }
}
using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    [TestFixture]
    public class BlockShadeUnitTests : IDrawingUnitTests
    {
        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleOutlineTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.CircleOutline(canvas, new SKPoint(50, 50), 40.0f);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(CircleOutlineTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Circle(canvas, new SKPoint(50, 50), 45.0f);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(CircleTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void FillTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Fill(canvas);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(FillTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void LineTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Line(canvas, new SKPoint(0, 0), new SKPoint(100, 100), 2);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(LineTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void PointTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Point(canvas, new SKPoint(50, 50));

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(PointTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void RectangleTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Rectangle(canvas, new SKPoint(25, 25), 50, 50);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(RectangleTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void ShapeOutlineTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.ShapeOutline(canvas, edgePoints);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(ShapeOutlineTest)}-{testIndex:00}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void ShapeTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Shape(canvas, edgePoints);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(ShapeTest)}-{testIndex:00}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleOutlineTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.TriangleOutline(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(TriangleOutlineTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 16));
            instance.Triangle(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(TriangleTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void WeightedPointTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new BlockShade(new SKColor(127, 0, 0, 51));
            instance.WeightedPoint(canvas, new SKPoint(50, 50), 5);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = System.IO.File.OpenWrite($"{nameof(BlockShadeUnitTests)}-{nameof(WeightedPointTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }
    }
}
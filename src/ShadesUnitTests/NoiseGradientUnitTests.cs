using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    public class NoiseGradientUnitTests : IDrawingUnitTests
    {
        private readonly IReadOnlyDictionary<int, SKColor> colorPoints = new Dictionary<int, SKColor>()
        {
            { 0, new SKColor(255, 0, 0, 16) },
            { 50, new SKColor(0, 255, 0, 16) },
            { 100, new SKColor(0, 0, 255, 16) }
        };

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleOutlineTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.CircleOutline(canvas, new SKPoint(50, 50), 40.0f);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(CircleOutlineTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Circle(canvas, new SKPoint(50, 50), 40.0f);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(CircleTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        private void CircleTestImpl(GradientAxis axis)
        {
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void FillTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Fill(canvas);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(FillTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void LineTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Line(canvas, new SKPoint(0, 0), new SKPoint(100, 100), 2);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(LineTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void PointTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Point(canvas, new SKPoint(50, 50));

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(PointTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void RectangleTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Rectangle(canvas, new SKPoint(25, 25), 50, 50);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(RectangleTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void ShapeOutlineTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.ShapeOutline(canvas, edgePoints);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(ShapeOutlineTest)}-{testIndex:00}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void ShapeTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Shape(canvas, edgePoints);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(ShapeTest)}-{testIndex:00}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleOutlineTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.TriangleOutline(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(TriangleOutlineTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.Triangle(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(TriangleTest)}.png");
            data.SaveTo(stream);

            Assert.IsTrue(true);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void WeightedPointTest()
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new NoiseGradient(new SKColor(0, 255, 0, 16), 10, 70);
            instance.WeightedPoint(canvas, new SKPoint(50, 50), 5);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = System.IO.File.OpenWrite($"{nameof(NoiseGradientUnitTests)}-{nameof(WeightedPointTest)}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }
    }
}
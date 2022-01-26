using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    public class LinearGradientUnitTests : IDrawingUnitTests
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
            CircleOutlineTestImpl(GradientAxis.Horizontal);
            CircleOutlineTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void CircleOutlineTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis, 0);
            instance.CircleOutline(canvas, new SKPoint(50, 50), 40.0f);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(CircleOutlineTest)}-{axis}.png"))
            {
                data.SaveTo(stream);
            }
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void CircleTest()
        {
            CircleTestImpl(GradientAxis.Horizontal);
            CircleTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void CircleTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis, 0);
            instance.Circle(canvas, new SKPoint(50, 50), 40.0f);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(CircleTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void FillTest()
        {
            FillTestImpl(GradientAxis.Horizontal);
            FillTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void FillTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.Fill(canvas);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(FillTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void LineTest()
        {
            LineTestImpl(GradientAxis.Horizontal);
            LineTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void LineTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.Line(canvas, new SKPoint(0, 0), new SKPoint(100, 100), 2);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(LineTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void PointTest()
        {
            PointTestImpl(GradientAxis.Horizontal);
            PointTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void PointTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.Point(canvas, new SKPoint(50, 50));

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(PointTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void RectangleTest()
        {
            RectangleTestImpl(GradientAxis.Horizontal);
            RectangleTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void RectangleTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.Rectangle(canvas, new SKPoint(25, 25), 50, 50);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(RectangleTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        public void ShapeOutlineTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            ShapeOutlineTestImpl(GradientAxis.Horizontal, edgePoints, testIndex);
            ShapeOutlineTestImpl(GradientAxis.Vertical, edgePoints, testIndex);

            Assert.IsTrue(true);
        }

        private void ShapeOutlineTestImpl(GradientAxis axis, IList<SKPoint> edgePoints, int testIndex)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.ShapeOutline(canvas, edgePoints);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(ShapeOutlineTest)}-{axis}-{testIndex:00}.png");
            data.SaveTo(stream);
        }

        [TestCaseSource(typeof(TestDataSources), nameof(TestDataSources.GetShapeEdgeData))]
        public void ShapeTest(IList<SKPoint> edgePoints, Queue<SKPoint> expected, int testIndex)
        {
            ShapeTestImpl(GradientAxis.Horizontal, edgePoints, testIndex);
            ShapeTestImpl(GradientAxis.Vertical, edgePoints, testIndex);

            Assert.IsTrue(true);
        }

        private void ShapeTestImpl(GradientAxis axis, IList<SKPoint> edgePoints, int testIndex)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.Shape(canvas, edgePoints);

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(ShapeTest)}-{axis}-{testIndex:00}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleOutlineTest()
        {
            TriangleOutlineTestImpl(GradientAxis.Horizontal);
            TriangleOutlineTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void TriangleOutlineTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.TriangleOutline(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(TriangleOutlineTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void TriangleTest()
        {
            TriangleTestImpl(GradientAxis.Horizontal);
            TriangleTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        private void TriangleTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.Triangle(canvas, new SKPoint(50, 25), new SKPoint(75, 75), new SKPoint(25, 75));

            using var data = canvas.Encode(SKEncodedImageFormat.Png, 80);
            using var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(TriangleTest)}-{axis}.png");
            data.SaveTo(stream);
        }

        [TestCase]
        [Description("The method being tested does not return a value. The result of the graphic operation are saved to disk for manual verification.")]
        public void WeightedPointTest()
        {
            WeightedPointTestImpl(GradientAxis.Horizontal);
            WeightedPointTestImpl(GradientAxis.Vertical);

            Assert.IsTrue(true);
        }

        public void WeightedPointTestImpl(GradientAxis axis)
        {
            var canvas = Shade.Canvas(100, 100, SKColors.White);

            var instance = new LinearGradient(colorPoints, axis);
            instance.WeightedPoint(canvas, new SKPoint(50, 50), 5);

            using (var data = canvas.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = System.IO.File.OpenWrite($"{nameof(LinearGradientUnitTests)}-{nameof(WeightedPointTest)}-{axis}.png"))
            {
                data.SaveTo(stream);
            }

            Assert.IsTrue(true);
        }
    }
}
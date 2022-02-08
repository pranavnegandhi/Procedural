using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    [TestFixture]
    public class ColorUtilitiesTests
    {
        private static IEnumerable<TestCaseData> ColorClampSimpleData()
        {
            byte red = 255;
            byte green = 128;
            byte blue = 18;
            var expected = new SKColor(red, green, blue);
            yield return new TestCaseData(red, green, blue).Returns(expected);
        }

        [TestCaseSource(typeof(ColorUtilitiesTests), nameof(ColorClampSimpleData))]
        public SKColor ColorClampSimpleTest(byte red, byte green, byte blue)
        {
            return new SKColor(red, green, blue);
        }

        private static IEnumerable<TestCaseData> ColorClampOverflowData()
        {
            int red = 256;
            int green = 128;
            int blue = 18;
            int alpha = 255;
            var expected = new SKColor(255, (byte)green, (byte)blue, (byte)alpha);
            yield return new TestCaseData(red, green, blue, alpha).Returns(expected);

            red = 46;
            green = 300;
            blue = 78;
            alpha = 255;
            expected = new SKColor((byte)red, 255, (byte)blue, (byte)alpha);
            yield return new TestCaseData(red, green, blue, alpha).Returns(expected);

            red = 46;
            green = 187;
            blue = 290;
            alpha = 255;
            expected = new SKColor((byte)red, (byte)green, 255, (byte)alpha);
            yield return new TestCaseData(red, green, blue, alpha).Returns(expected);

            red = 46;
            green = 129;
            blue = 78;
            alpha = 563;
            expected = new SKColor((byte)red, (byte)green, (byte)blue, 255);
            yield return new TestCaseData(red, green, blue, alpha).Returns(expected);
        }

        [TestCaseSource(typeof(ColorUtilitiesTests), nameof(ColorClampOverflowData))]
        public SKColor ColorClampOverflowTest(int red, int green, int blue, int alpha)
        {
            return ColorUtilities.ColorClamp(red, green, blue, alpha);
        }

        private static IEnumerable<TestCaseData> ColorClampFData()
        {
            var red = 255.0f;
            var green = 128.0f;
            var blue = 18.0f;
            var expected = new SKColor(255, 128, 18);
            yield return new TestCaseData(red, green, blue).Returns(expected);
        }

        [TestCaseSource(typeof(ColorUtilitiesTests), nameof(ColorClampFData))]
        public SKColor ColorClampFTest(float red, float green, float blue)
        {
            return ColorUtilities.ColorClamp(red, green, blue, 0.0f);
        }
    }
}
using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace Notadesigner.Shades.Tests
{
    [TestFixture]
    public class GeometryUtilitiesTests
    {
        private const double Tolerance = 0.01;

        private static IEnumerable<TestCaseData> DistanceBetweenPointsSimpleData()
        {
            var point1 = new SKPoint(-7.3f, -4.8f);
            var point2 = new SKPoint(17.2f, 6.5f);
            var expected = 26.99;
            yield return new TestCaseData(point1, point2, expected);
        }

        [TestCaseSource(typeof(GeometryUtilitiesTests), nameof(DistanceBetweenPointsSimpleData))]
        public void DistanceBetweenPointsSimpleTest(SKPoint point1, SKPoint point2, double expected)
        {
            var actual = GeometryUtilities.DistanceBetweenPoints(point1, point2);
            Assert.That(actual, Is.EqualTo(expected).Within(Tolerance));
        }

        private static IEnumerable<TestCaseData> DistanceBetweenPointsIData()
        {
            var point1 = new SKPointI(-7, -5);
            var point2 = new SKPointI(17, 6);
            var expected = 26;
            yield return new TestCaseData(point1, point2, expected);
        }

        [TestCaseSource(typeof(GeometryUtilitiesTests), nameof(DistanceBetweenPointsIData))]
        public void DistanceBetweenPointsITest(SKPointI point1, SKPointI point2, int expected)
        {
            var actual = GeometryUtilities.DistanceBetweenPoints(point1, point2);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
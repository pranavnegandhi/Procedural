using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Notadesigner.Shades
{
    /// <summary>
    /// An Abstract base clase Shade. Methods are used to mark shapes onto images according to various color rules.
    /// </summary>
    public abstract class Shade
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="warpSize">How much warp noise is allowed to alter the mark in pixels.</param>
        public Shade(int warpSize = 0)
        {
            WarpNoises[0] = new NoiseField();
            WarpNoises[1] = new NoiseField();
            WarpSize = warpSize;
        }

        /// <summary>
        /// Returns a blank image to draw on.
        /// </summary>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="color">Desired RGB colour of the background.</param>
        /// <returns>An instance of <see cref="SKBitmap"/></returns>
        public static SKBitmap Canvas(int width = 100, int height = 100, SKColor color = default)
        {
            var canvas = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas.Erase(color);

            return canvas;
        }

        public NoiseField[] WarpNoises
        {
            get;
            set;
        } = new NoiseField[2];

        public int WarpSize
        {
            get;
            set;
        }

        /// <summary>
        /// Determines the shade/color for the given coordinates.
        /// </summary>
        /// <param name="point">Coordinates in the form of a point.</param>
        /// <returns>Color in form of RGB.</returns>
        public abstract SKColor DetermineShade(SKPoint point);

        /// <summary>
        /// If transparency settings are applied, appropriately adjusts color.
        /// </summary>
        /// <param name="point">Canvas coordinates.</param>
        /// <param name="canvas">Image mark is to be made on.</param>
        /// <param name="color">Initial color before transparency has been applied.</param>
        /// <returns>Color in form of RGB.</returns>
        public SKColor ApplyTransparency(SKPoint point, SKBitmap canvas, SKColor color)
        {
            var x = Convert.ToInt32(point.X);
            x = Math.Max(0, Math.Min(canvas.Width - 1, x));
            var y = Convert.ToInt32(point.Y);
            y = Math.Max(0, Math.Min(canvas.Height - 1, y));

            var initialColor = canvas.GetPixel(x, y);
            var alpha = 1.0f * color.Alpha / 255.0f;
            var newRed = (byte)(initialColor.Red + ((color.Red - initialColor.Red) * alpha));
            var newGreen = (byte)(initialColor.Green + ((color.Green - initialColor.Green) * alpha));
            var newBlue = (byte)(initialColor.Blue + ((color.Blue - initialColor.Blue) * alpha));
            var newColor = new SKColor(newRed, newGreen, newBlue);

            return newColor;
        }

        /// <summary>
        /// If warp is applied in shade, appropriately adjusts location of point.
        /// </summary>
        /// <param name="point">Canvas coordinates.</param>
        /// <returns>Canvas coordinates.</returns>
        public SKPoint AdjustPoint(SKPoint point)
        {
            var x = (float)(point.X + (WarpNoises[0].Noise(point) * WarpSize));
            var y = (float)(point.Y + (WarpNoises[1].Noise(point) * WarpSize));

            return new SKPoint(x, y);
        }

        /// <summary>
        /// Determines colour and draws a point on an image.
        /// </summary>
        /// <param name="canvas">Image to draw point on.</param>
        /// <param name="point">Canvas coordinates</param>
        public void Point(SKBitmap canvas, SKPoint point)
        {
            var color = DetermineShade(point);
            if (WarpSize != 0)
            {
                AdjustPoint(point);
            }

            color = ApplyTransparency(point, canvas, color);

            var x = Convert.ToInt32(point.X);
            var y = Convert.ToInt32(point.Y);

            if ((x < 0 || x >= canvas.Width) || (y < 0 || y >= canvas.Height))
            {
                return;
            }

            canvas.SetPixel(x, y, color);
        }

        /// <summary>
        /// Determines colour and draws a weighted point on an image.
        /// </summary>
        /// <param name="canvas">Image to draw point on.</param>
        /// <param name="point">Canvas coordinates.</param>
        /// <param name="weight">Weight of point.</param>
        public void WeightedPoint(SKBitmap canvas, SKPoint point, int weight)
        {
            var color = DetermineShade(point);
            if (WarpSize != 0)
            {
                point = AdjustPoint(point);
            }

            color = ApplyTransparency(point, canvas, color);

            for (var row = 0; row < weight; row++)
            {
                for (var col = 0; col < weight; col++)
                {
                    var x = Convert.ToInt32(point.X) + col;
                    var y = Convert.ToInt32(point.Y) + row;

                    if ((x < 0 || x >= canvas.Width) || (y < 0 || y >= canvas.Height))
                    {
                        continue;
                    }

                    canvas.SetPixel(x, y, color);
                }
            }
        }

        /// <summary>
        /// Returns a list of  pixels from inside a edge of points using ray casting algorithm.
        /// https://en.wikipedia.org/wiki/Point_in_polygon
        /// Vertex correction still needs some perfecting, unusual or particularly angular shapes may cause difficulties.
        /// </summary>
        /// <param name="edgePixels">A collection of coordinates making up one edge of the polygon.</param>
        /// <returns>A collection of pixels within the edge.</returns>
        public ICollection<SKPoint> PixelsInsideEdge(ICollection<SKPoint> edgePixels)
        {
            /// Contains a list of distinct values along the X axis, extracted from edgePixels.
            /// Each unique value along the X axis corresponds to a list of unique values along
            /// the Y axis that intersect with the X coordinate.
            var xs = new SortedDictionary<int, SortedSet<int>>();
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            foreach (var e in edgePixels)
            {
                var ex = Convert.ToInt32(e.X);
                var ey = Convert.ToInt32(e.Y);
                maxX = Math.Max(maxX, ex);
                minX = Math.Min(minX, ex);
                maxY = Math.Max(maxY, ey);
                minY = Math.Min(minY, ey);

                if (xs.TryGetValue(ex, out var points))
                {
                    points.Add(ey);
                }
                else
                {
                    xs[ex] = new SortedSet<int>() { ey };
                }
            }

            /// Contains a list of points that make up the inside of the shape formed by the
            /// bounds of edgePixels.
            var innerPixels = new List<SKPoint>();
            for (var x = minX; x <= maxX; x++)
            {
                var ys = xs[x];

                /// Find the lowest values along the Y axis, i.e. values
                /// that make up the lower edge of the shape.
                var temp = new SortedSet<int>();
                foreach (var y in ys)
                {
                    if (!ys.TryGetValue(y - 1, out var _))
                    {
                        temp.Add(y);
                    }
                }

                var rayCount = 0;
                for (var y = temp.Min; y <= temp.Max; y++)
                {
                    if (temp.TryGetValue(y, out var _))
                    {
                        rayCount++;
                    }

                    if (rayCount % 2 == 1)
                    {
                        innerPixels.Add(new SKPoint(x, y));
                    }
                }
            }

            innerPixels.AddRange(edgePixels);

            return innerPixels;
        }

        /// <summary>
        /// Returns a list of points that form a straight line between two points.
        /// </summary>
        /// <param name="point1">Coordinates for first point.</param>
        /// <param name="point2">Coordinates for second point.</param>
        /// <returns>List of points between the two points.</returns>
        public ICollection<SKPoint> PixelsBetweenTwoPoints(SKPoint point1, SKPoint point2)
        {
            float xStep, yStep, iStop;
            if (Math.Abs(point1.X - point2.X) > Math.Abs(point1.Y - point2.Y))
            {
                xStep = (point1.X > point2.X) ? -1 : 1;
                yStep = Math.Abs(point1.Y - point2.Y) / Math.Abs(point1.X - point2.X);

                if (point1.Y > point2.Y)
                {
                    yStep *= -1;
                }

                iStop = Math.Abs(point1.X - point2.X);
            }
            else
            {
                yStep = (point1.Y > point2.Y) ? -1 : 1;
                xStep = Math.Abs(point1.X - point2.X) / Math.Abs(point1.Y - point2.Y);
                if (point1.X > point2.X)
                {
                    xStep *= -1;
                }

                iStop = Math.Abs(point1.Y - point2.Y);
            }

            var points = new List<SKPoint>();
            var x = point1.X;
            var y = point1.Y;

            for (var i = 0; i < Convert.ToInt32(iStop) + 1; i++)
            {
                points.Add(new SKPoint(Convert.ToInt32(x), Convert.ToInt32(y)));
                x += xStep;
                y += yStep;
            }

            return points;
        }

        /// <summary>
        /// Draws a weighted line on the image.
        /// </summary>
        /// <param name="canvas">Image to draw on.</param>
        /// <param name="point1">Coordinates for the start of the line.</param>
        /// <param name="point2">Coordinates for the end of the line.</param>
        /// <param name="weight">Thickness of the line in pixels.</param>
        public void Line(SKBitmap canvas, SKPoint point1, SKPoint point2, int weight = 2)
        {
            var points = PixelsBetweenTwoPoints(point1, point2);
            foreach (var p in points)
            {
                WeightedPoint(canvas, p, weight);
            }
        }

        /// <summary>
        /// Fills the image with colour.
        /// </summary>
        /// <param name="canvas">Image to fill the colour on.</param>
        public virtual void Fill(SKBitmap canvas)
        {
            var warpSize = WarpSize;
            WarpSize = 0;
            var point = new SKPoint(0, 0);
            for (var y = 0; y < canvas.Height; y++)
            {
                point.Y = y;

                for (var x = 0; x < canvas.Width; x++)
                {
                    point.X = x;
                    Point(canvas, point);
                }
            }

            WarpSize = warpSize;
        }

        /// <summary>
        /// Returns a list of coordinates making up the edge of the shape.
        /// </summary>
        /// <param name="points">Coordinates making up the vertexes of the shape.</param>
        /// <returns>Coordinates making up the edge of the shape.</returns>
        public ICollection<SKPoint> GetShapeEdge(IList<SKPoint> points)
        {
            /// Find the closing edge between the start and end points
            var edge = PixelsBetweenTwoPoints(points[points.Count - 1], points[0]);

            /// Find the edges between each point in the shape
            for (var i = 0; i < points.Count - 1; i++)
            {
                var singleEdge = PixelsBetweenTwoPoints(points[i], points[i + 1]);
                foreach (var e in singleEdge)
                {
                    edge.Add(e);
                }
            }

            return edge;
        }

        /// <summary>
        /// Draws a shape on an image based on a list of points.
        /// </summary>
        /// <param name="canvas">Image to draw on.</param>
        /// <param name="points">A list of coordinates with which to make the shape.</param>
        public void Shape(SKBitmap canvas, IList<SKPoint> points)
        {
            var outerEdges = GetShapeEdge(points);
            var innerEdges = PixelsInsideEdge(outerEdges);

            foreach (var pixel in innerEdges)
            {
                Point(canvas, pixel);
            }
        }

        /// <summary>
        /// Draws a shape outline on an image based on a list of points.
        /// </summary>
        /// <param name="canvas">Image to draw on.</param>
        /// <param name="edgePoints">A list of coordinates with which to make the shape.</param>
        public void ShapeOutline(SKBitmap canvas, IList<SKPoint> points, int weight = 1)
        {
            var outerEdges = GetShapeEdge(points);

            foreach (var pixel in outerEdges)
            {
                WeightedPoint(canvas, pixel, weight);
            }
        }

        /// <summary>
        /// Draws a rectangle on the image.
        /// </summary>
        /// <param name="canvas">Image to draw on.</param>
        /// <param name="origin">Top left corner of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public void Rectangle(SKBitmap canvas, SKPoint origin, float width, float height)
        {
            var point = new SKPoint();
            for (var x = origin.X; x < origin.X + width; x++)
            {
                for (var y = origin.Y; y < origin.Y + height; y++)
                {
                    point.X = x;
                    point.Y = y;
                    Point(canvas, point);
                }
            }
        }

        /// <summary>
        /// Draws a triangle on the image. This is the same as calling the <see cref="Shape(SKBitmap, IList{SKPoint})"/> method with three points.
        /// </summary>
        /// <param name="canvas">The image to draw on.</param>
        /// <param name="point1">Coordinates for the first point of the triangle.</param>
        /// <param name="point2">Coordinates for the second point of the triangle.</param>
        /// <param name="point3">Coordinates for the third point of the triangle.</param>
        public void Triangle(SKBitmap canvas, SKPoint point1, SKPoint point2, SKPoint point3)
        {
            var points = new List<SKPoint>()
            {
                point1, point2, point3
            };

            Shape(canvas, points);
        }

        /// <summary>
        /// Draws a triangle outline on the image. This is the same as calling the <see cref="ShapeOutline(SKBitmap, IList{SKPoint})"/> method with three points.
        /// </summary>
        /// <param name="canvas">The image to draw on.</param>
        /// <param name="point1">Coordinates for the first point of the triangle.</param>
        /// <param name="point2">Coordinates for the second point of the triangle.</param>
        /// <param name="point3">Coordinates for the third point of the triangle.</param>
        public void TriangleOutline(SKBitmap canvas, SKPoint point1, SKPoint point2, SKPoint point3, int weight = 1)
        {
            var points = new List<SKPoint>()
            {
                point1, point2, point3
            };

            ShapeOutline(canvas, points, weight);
        }

        /// <summary>
        /// Returns the edge coordinates of a circle.
        /// </summary>
        /// <param name="origin">Centre of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <returns>Coordinates making up the edge of the circle.</returns>
        public ICollection<SKPoint> GetCircleEdge(SKPoint origin, float radius)
        {
            var edgePixels = new List<SKPoint>();
            var circumference = radius * 2 * Math.PI;
            for (var c = 0; c < circumference + 1; c++)
            {
                var angle = (c / circumference) * 360;
                angle = angle * Math.PI / 180;
                var opposite = Convert.ToSingle(Math.Sin(angle) * radius);
                var adjacent = Convert.ToSingle(Math.Cos(angle) * radius);
                var coordinate = new SKPoint(origin.X + adjacent, origin.Y + opposite);
                edgePixels.Add(coordinate);
            }

            return edgePixels;
        }

        /// <summary>
        /// Draws a circle on the image.
        /// </summary>
        /// <param name="canvas">The image to draw on.</param>
        /// <param name="origin">Centre of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        public void Circle(SKBitmap canvas, SKPoint origin, float radius)
        {
            var outerEdges = GetCircleEdge(origin, radius);
            var innerEdges = PixelsInsideEdge(outerEdges);

            foreach (var pixel in innerEdges)
            {
                Point(canvas, pixel);
            }
        }

        /// <summary>
        /// Draws a circle on the image.
        /// </summary>
        /// <param name="canvas">The image to draw on.</param>
        /// <param name="origin">Centre of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        public void CircleOutline(SKBitmap canvas, SKPoint origin, float radius, int weight = 1)
        {
            var outerEdges = GetCircleEdge(origin, radius);

            foreach (var pixel in outerEdges)
            {
                WeightedPoint(canvas, pixel, weight);
            }
        }
    }
}
using Notadesigner.Shades;
using SkiaSharp;
using System;
using System.Diagnostics;

namespace Notadesigner.Effects
{
    public class Carousel : EffectBase
    {
        private readonly SKBitmap _bitmap;

        private readonly NumericParameter<byte> _weightParameter = new();

        private readonly ColorParameter _color0 = new();

        private readonly ColorParameter _color1 = new();

        private readonly ColorParameter _color2 = new();

        private readonly ColorParameter _color3 = new();

        private readonly ColorParameter _color4 = new();

        private readonly ColorParameter _color5 = new();

        private int _indexX = 0;

        private int _indexY = 0;

        private int _numX;

        private int _numY;

        private Random _gen;

        private SKPoint p1;

        private SKPoint p2;

        private static readonly BlockShade[] Inks = new BlockShade[]
        {
                new BlockShade(new SKColor(12, 11, 6, 255), 0),
                new BlockShade(new SKColor(240, 203, 71, 255), 0),
                new BlockShade(new SKColor(58, 135, 163, 255), 0),
                new BlockShade(new SKColor(235, 177, 176, 255), 0),
                new BlockShade(new SKColor(244, 238, 224, 255), 0),
                new BlockShade(new SKColor(200, 38, 49, 255), 0)
        };

        private static readonly ColorParameter[] ColorParams = new ColorParameter[6];

        public Carousel(SKBitmap bitmap)
        {
            _bitmap = bitmap;

            _weightParameter.Text = "Weight";
            _weightParameter.MaxValue = byte.MaxValue;
            _weightParameter.MinValue = 1;
            _weightParameter.Value = 20;

            _color0.Text = "Color 1";
            _color0.Value = new SKColor(143, 81, 77, 255);
            ColorParams[0] = _color0;

            _color1.Text = "Color 2";
            _color1.Value = new SKColor(219, 93, 85, 255);
            ColorParams[1] = _color1;

            _color2.Text = "Color 3";
            _color2.Value = new SKColor(96, 168, 219, 255);
            ColorParams[2] = _color2;

            _color3.Text = "Color 4";
            _color3.Value = new SKColor(143, 139, 49, 255);
            ColorParams[3] = _color3;

            _color4.Text = "Color 5";
            _color4.Value = new SKColor(219, 214, 86, 255);
            ColorParams[4] = _color4;

            _color5.Text = "Color 6";
            _color5.Value = new SKColor(200, 38, 49, 255);
            ColorParams[5] = _color5;

            Parameters = new(_weightParameter, _color0, _color1, _color2, _color3, _color4, _color5);
        }

        public override string ToString() => nameof(Carousel);

        public override void Execute()
        {
            var weight = _weightParameter.Value;

            if (_indexX < _numX)
            {
                p2.X = p1.X + _gen.Next(1, 5) * weight;
                var ink = Inks[_gen.Next(Inks.Length)];
                _bitmap.Line(ink, p1, p2, weight);
                p1.X = p2.X;

                if (p1.X > _bitmap.Width)
                {
                    var y = _gen.Next(0, (_bitmap.Height - weight) / weight) * weight;

                    p1.X = p2.X = 0;
                    p1.Y = p2.Y = y;

                    _indexX++;

                    if (_indexX >= _numX)
                    {
                        var x = _gen.Next(0, (_bitmap.Width - weight) / weight) * weight;

                        p1.X = p2.X = x;
                        p1.Y = p2.Y = 0;
                    }
                }
            }
            else if (_indexY < _numY)
            {
                {
                    p2.Y = p1.Y + _gen.Next(1, 5) * weight;
                    var ink = Inks[_gen.Next(Inks.Length)];
                    _bitmap.Line(ink, p1, p2, weight);
                    p1.Y = p2.Y;
                }

                if (p1.Y > _bitmap.Height)
                {
                    var x = _gen.Next(0, (_bitmap.Width - weight) / weight) * weight;

                    p1.X = p2.X = x;
                    p1.Y = p2.Y = 0;

                    _indexY++;
                }
            }
            else
            {
                OnFinished();
            }
        }

        public override void Reset()
        {
            var seed = (int)DateTime.UtcNow.Ticks;
            _gen = new Random(seed);
            Trace.TraceInformation($"seed: {seed}");
            _numX = _gen.Next(2, 15);
            _numY = _gen.Next(2, 15);
            _indexX = 0;
            _indexY = 0;

            p1 = new SKPoint();
            p2 = new SKPoint();

            var weight = _weightParameter.Value;
            var y = _gen.Next(0, (_bitmap.Height - weight) / weight) * weight;

            p1.X = p2.X = 0;
            p1.Y = p2.Y = y;

            for (var i = 0; i < Inks.Length; i++)
            {
                Inks[i].Color = ColorParams[i].Value;
            }

            _bitmap.Erase(SKColors.White);
        }
    }
}
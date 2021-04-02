using SkiaSharp;
using System;
using System.Runtime.CompilerServices;

namespace Desktop
{
    public class AnimatedLines
    {
        private static readonly Random _gen = new Random((int)DateTime.Now.Millisecond);

        private readonly SKImageInfo _info;

        private readonly SKSurface _surface;

        private readonly SKCanvas _canvas;

        private int _div1;

        private int _div2;

        private int _div3;

        private int _div4;

        private int _mul1;

        private int _mul2;

        private int _mul3;

        private int _mul4;

        private int _time;

        private int _start;

        private int _end;

        private SKPaint _ink;

        private int _index;

        public AnimatedLines(SKImageInfo info, SKSurface surface)
        {
            _info = info;
            _surface = surface;
            _canvas = _surface.Canvas; // _surface.Canvas;
        }

        public void Initialize(SKColor color)
        {
            _ink = new SKPaint()
            {
                Color = color,
                StrokeWidth = 1,
                StrokeCap = SKStrokeCap.Round,
                IsAntialias = true,
                IsStroke = true
            };

            Reset();
        }

        private const int MinDivisor = 8;

        private const int MaxDivisor = 16;

        private const int MinMultiplier = 100;

        private const int MaxMultiplier = 200;

        public void Reset()
        {
            _div1 = _gen.Next(MinDivisor, MaxDivisor);
            _div2 = _gen.Next(MinDivisor, MaxDivisor);
            _div3 = _gen.Next(MinDivisor, MaxDivisor);
            _div4 = _gen.Next(MinDivisor, MaxDivisor);
            _mul1 = _gen.Next(MinMultiplier, MaxMultiplier);
            _mul2 = _gen.Next(MinMultiplier, MaxMultiplier);
            _mul3 = _gen.Next(MinMultiplier, MaxMultiplier);
            _mul4 = _gen.Next(MinMultiplier, MaxMultiplier);
            _time = 0;
            _start = _gen.Next(0, 9999999);
            _end = _gen.Next(1, 1500);
            _index = 0;

            _canvas.Clear(new SKColor(0x00, 0x00, 0x00));
        }

        public int DrawNextFrame()
        {
            if (_index >= _end)
            {
                return -1;
            }

            _index += 10;
            _time = _start + _index;
            var p1 = new SKPoint(_info.Width / 2 + GetX(_time / 15, _div1, _mul1, _div2, _mul2), _info.Height / 2 + GetY(_time / 15, _div3, _mul3, _div4, _mul4));
            var p2 = new SKPoint(_info.Width / 2 + GetX(_time / 12, _div1, _mul1, _div2, _mul2), _info.Height / 2 + GetY(_time / 12, _div3, _mul3, _div4, _mul4));
            _canvas.DrawLine(p1, p2, _ink);

            return _index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetX(float ft, int div1, int mul1, int div2, int mul2) => (float)(Math.Sin(ft / div1) * mul1 + Math.Sin(ft / div2) * mul2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetY(float ft, int div1, int mul1, int div2, int mul2) => (float)(Math.Cos(ft / div1) * mul1 + Math.Cos(ft / div2) * mul2);
    }
}
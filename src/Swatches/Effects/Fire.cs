using SkiaSharp;
using System;

namespace Notadesigner.Effects
{
    public class Fire : EffectBase
    {
        private static readonly Random _gen = new(DateTime.UtcNow.Millisecond);

        private readonly SKBitmap _destBitmap;

        private SKBitmap _bitmap;

        private static int _width = 1;

        private static int _height = 1;

        private static int[] _pixels;

        private static readonly SKColor[] Palette =
        {
                new SKColor(0x07, 0x07, 0x07, 0xff), new SKColor(0x1F, 0x07, 0x07, 0xff),
                new SKColor(0x2F, 0x0F, 0x07, 0xff), new SKColor(0x47, 0x0F, 0x07, 0xff),
                new SKColor(0x57, 0x17, 0x07, 0xff), new SKColor(0x67, 0x1F, 0x07, 0xff),
                new SKColor(0x77, 0x1F, 0x07, 0xff), new SKColor(0x8F, 0x27, 0x07, 0xff),
                new SKColor(0x9F, 0x2F, 0x07, 0xff), new SKColor(0xAF, 0x3F, 0x07, 0xff),
                new SKColor(0xBF, 0x47, 0x07, 0xff), new SKColor(0xC7, 0x47, 0x07, 0xff),
                new SKColor(0xDF, 0x4F, 0x07, 0xff), new SKColor(0xDF, 0x57, 0x07, 0xff),
                new SKColor(0xDF, 0x57, 0x07, 0xff), new SKColor(0xD7, 0x5F, 0x07, 0xff),
                new SKColor(0xD7, 0x5F, 0x07, 0xff), new SKColor(0xD7, 0x67, 0x0F, 0xff),
                new SKColor(0xCF, 0x6F, 0x0F, 0xff), new SKColor(0xCF, 0x77, 0x0F, 0xff),
                new SKColor(0xCF, 0x7F, 0x0F, 0xff), new SKColor(0xCF, 0x87, 0x17, 0xff),
                new SKColor(0xC7, 0x87, 0x17, 0xff), new SKColor(0xC7, 0x8F, 0x17, 0xff),
                new SKColor(0xC7, 0x97, 0x1F, 0xff), new SKColor(0xBF, 0x9F, 0x1F, 0xff),
                new SKColor(0xBF, 0x9F, 0x1F, 0xff), new SKColor(0xBF, 0xA7, 0x27, 0xff),
                new SKColor(0xBF, 0xA7, 0x27, 0xff), new SKColor(0xBF, 0xAF, 0x2F, 0xff),
                new SKColor(0xB7, 0xAF, 0x2F, 0xff), new SKColor(0xB7, 0xB7, 0x2F, 0xff),
                new SKColor(0xB7, 0xB7, 0x37, 0xff), new SKColor(0xCF, 0xCF, 0x6F, 0xff),
                new SKColor(0xDF, 0xDF, 0x9F, 0xff), new SKColor(0xEF, 0xEF, 0xC7, 0xff),
                new SKColor(0xFF, 0xFF, 0xFF, 0xff)
        };

        private readonly NumericParameter<int> _widthParameter = new();

        private readonly NumericParameter<int> _heightParameter = new();

        private readonly NumericParameter<int> _durationInSeconds = new();

        private int _totalFrames = 0;

        private int _currentFrame = 0;

        public Fire(SKBitmap bitmap)
        {
            _destBitmap = bitmap;

            _widthParameter.MaxValue = 512;
            _widthParameter.MinValue = 1;
            _widthParameter.Value = Math.Min(512, Math.Max(1, _destBitmap.Width));
            _widthParameter.Text = "Width";

            _heightParameter.MaxValue = 512;
            _heightParameter.MinValue = 1;
            _heightParameter.Value = Math.Min(512, Math.Max(1, _destBitmap.Height));
            _heightParameter.Text = "Height";

            _durationInSeconds.MaxValue = 60;
            _durationInSeconds.MinValue = 1;
            _durationInSeconds.Value = 2;
            _durationInSeconds.Text = "Duration (secs)";

            Parameters = new(_widthParameter, _heightParameter, _durationInSeconds);
        }

        public override void Execute()
        {
            var counter = 0;
            var random = _gen.Next(0, 255);
            var curSrc = _width;

            do
            {
                var srcOffset = curSrc + counter;
                var pixel = _pixels[srcOffset];
                var step = 2;
                random = SpreadFire(pixel, curSrc, counter, srcOffset, random, _width);
                curSrc += _width;
                srcOffset += _width;

                do
                {
                    pixel = _pixels[srcOffset];
                    step += 2;
                    random = SpreadFire(pixel, curSrc, counter, srcOffset, random, _width);
                    pixel = _pixels[srcOffset + _width];
                    curSrc += _width;
                    srcOffset += _width;
                    random = SpreadFire(pixel, curSrc, counter, srcOffset, random, _width);
                    curSrc += _width;
                    srcOffset += _width;
                } while (step < _height);

                counter++;
                curSrc -= (_width * _height) - _width;
            } while (counter < _width);

            for (var h = 0; h < _height; h++)
            {
                for (var w = 0; w < _width; w++)
                {
                    var p = _pixels[h * _width + w];
                    var color = Palette[p];
                    _bitmap.SetPixel(w, h, color);
                }
            }

            _bitmap.ScalePixels(_destBitmap, SKFilterQuality.None);

            _currentFrame++;

            if (_currentFrame >= _totalFrames)
            {
                for (var h = _height - 1; h > _height - 5; h--)
                {
                    for (var w = 0; w < _width; w++)
                    {
                        if (_pixels[h * _width + w] > 0)
                        {
                            var color = Convert.ToInt32(_gen.NextDouble()) & 3;
                            _pixels[h * _width + w] -= color;
                        }
                    }
                }

                if (Array.TrueForAll(_pixels, i => i == 0))
                {
                    OnFinished();
                }
            }
        }

        private static int SpreadFire(int pixel, int curSrc, int counter, int srcOffset, int rand, int width)
        {
            if (pixel != 0)
            {
                var randIdx = _gen.Next(0, 255);
                rand = (rand + 2) & 255;
                var tmpSrc = curSrc + (((counter - (randIdx & 3)) + 1) & (width - 1));
                var value = pixel - (randIdx & 1);
                _pixels[tmpSrc - _width] = value;
            }
            else
            {
                _pixels[srcOffset - _width] = 0;
            }

            return rand;
        }

        public override void Reset()
        {
            _width = _widthParameter.Value;
            _height = _heightParameter.Value;
            _totalFrames = _durationInSeconds.Value * 30;
            _currentFrame = 0;

            _bitmap = new SKBitmap(_width, _height);
            _pixels = new int[_width * _height];

            Array.Clear(_pixels, 0, _width * _height);

            for (var i = 0; i < _width; i++)
            {
                _pixels[(_height - 1) * _width + i] = 36;
            }

            _bitmap.Erase(SKColors.Black);
        }
    }
}
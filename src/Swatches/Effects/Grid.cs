using Notadesigner.Shades;
using SkiaSharp;
using System;

namespace Notadesigner.Effects
{
    public class Grid : EffectBase
    {
        private readonly SKBitmap _bitmap;

        private readonly NumericParameter<int> _gridWidthParameter = new();

        private readonly NumericParameter<int> _gridHeightParameter = new();

        private readonly NumericParameter<int> _offsetParameter = new();

        private static readonly BlockShade[] Inks = new BlockShade[]
        {
            new BlockShade(new SKColor(252, 247, 67, 255)),
            new BlockShade(new SKColor(74,140,167, 255)),
            new BlockShade(new SKColor(109,181,197, 255)),
            new BlockShade(new SKColor(109,181,197, 255)),
            new BlockShade(new SKColor(0,155,202, 255)),
            new BlockShade(new SKColor(0,155,202, 255)),
            new BlockShade(new SKColor(0,155,202, 255)),
            new BlockShade(new SKColor(230,243,247, 255)),
            new BlockShade(new SKColor(17,92,112, 255)),
            new BlockShade(new SKColor(17,92,112, 255))
        };

        public Grid(SKBitmap bitmap)
        {
            _bitmap = bitmap;

            _gridWidthParameter.MaxValue = 255;
            _gridWidthParameter.MinValue = 1;
            _gridWidthParameter.Value = 150;

            _gridHeightParameter.MaxValue = 255;
            _gridHeightParameter.MinValue = 1;
            _gridHeightParameter.Value = 100;

            _offsetParameter.MaxValue = 100;
            _offsetParameter.MinValue = 10;
            _offsetParameter.Value = 100;

            Parameters = new(_gridWidthParameter, _gridHeightParameter, _offsetParameter);
        }

        public override string ToString() => nameof(Grid);

        public override void Execute()
        {
            var gridWidth = _gridWidthParameter.Value;
            var gridHeight = _gridHeightParameter.Value;
            var offset = _offsetParameter.Value;

            var i = 0;
            var _gen = new Random((int)DateTime.UtcNow.Ticks);

            for (var x = 0; x < _bitmap.Width + gridWidth; x += gridWidth)
            {
                for (var y = (i % gridHeight) - gridHeight; y < _bitmap.Height + gridHeight; y += gridHeight)
                {
                    var ink = Inks[_gen.Next(Inks.Length)]; ;
                    _bitmap.Triangle(ink, new SKPoint(x, y), new SKPoint(x, y + gridHeight), new SKPoint(x + gridWidth, y + offset));
                    _bitmap.Triangle(ink, new SKPoint(x, y + gridHeight), new SKPoint(x + gridWidth, y + offset), new SKPoint(x + gridWidth, y + gridHeight + offset));
                }

                i += offset;
            }

            OnFinished();
        }

        public override void Reset()
        {
            _bitmap.Erase(SKColors.White);
        }
    }
}
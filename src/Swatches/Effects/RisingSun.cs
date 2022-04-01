using Notadesigner.Shades;
using SkiaSharp;
using System;

namespace Notadesigner.Effects
{
    public class RisingSun : EffectBase
    {
        public override string ToString() => "Rising Sun";

        private readonly SKBitmap _bitmap;

        private readonly NumericParameter<float> _radiusParameter = new();

        private readonly ColorParameter _backgroundParameter = new();

        private readonly ColorParameter _foregroundParameter = new();

        private readonly SKPoint _origin;

        public RisingSun(SKBitmap bitmap)
        {
            _bitmap = bitmap;

            var shorter = Math.Min(_bitmap.Width, _bitmap.Height);
            var radius = shorter >> 1;

            var x = _bitmap.Width >> 1;
            var y = _bitmap.Height >> 1;
            _origin = new SKPoint(x, y);

            _radiusParameter.Text = "Radius";
            _radiusParameter.MaxValue = radius;
            _radiusParameter.MinValue = 1;
            _radiusParameter.Value = radius;

            _backgroundParameter.Text = "Background";
            _backgroundParameter.Value = new SKColor(0xff, 0xff, 0xff, 0xff);

            _foregroundParameter.Text = "Foreground";
            _foregroundParameter.Value = new SKColor(0xff, 0x00, 0x00, 0xff);

            Parameters = new(_radiusParameter, _backgroundParameter, _foregroundParameter);
        }

        public override void Execute()
        {
            var radius = Convert.ToSingle(_radiusParameter.Value);

            var background = new BlockShade(_backgroundParameter.Value);
            _bitmap.Fill(background);
            var foreground = new BlockShade(_foregroundParameter.Value);
            _bitmap.Circle(foreground, _origin, radius);

            OnFinished();
        }

        public override void Reset()
        {
            _bitmap.Erase(new SKColor(0xff, 0xff, 0xff, 0xff));
        }
    }
}
using Notadesigner.Shades;
using SkiaSharp;
using System;

namespace Notadesigner.Effects
{
    public class Circle : EffectBase
    {
        private readonly SKBitmap _bitmap;

        private float _lineWeight;

        private int _radius;

        private readonly ColorParameter _lineColorParamter = new();

        private readonly NumericParameter<float> _lineWeightParameter = new();

        private BlockShade _outline;

        public Circle(SKBitmap bitmap)
        {
            _bitmap = bitmap;

            _lineColorParamter.Value = SKColors.Black;
            _lineColorParamter.Text = "Color";

            _lineWeightParameter.MinValue = 0.01f;
            _lineWeightParameter.MaxValue = 20.0f;
            _lineWeightParameter.Value = 0.9f;
            _lineWeightParameter.Text = "Weight";

            Parameters = new(_lineWeightParameter, _lineColorParamter);
        }

        public override string ToString() => nameof(Circle);

        public override void Execute()
        {
            _bitmap.CircleOutline(_outline, new SKPoint(_bitmap.Width / 2, _bitmap.Height / 2), _radius, Convert.ToInt32(_lineWeight));
            _outline.WarpSize += 3;
            _lineWeight += 0.01f;
            _radius += 10;

            if (_radius >= _bitmap.Height - 200)
            {
                OnFinished();
            }
        }

        public override void Reset()
        {
            _outline = new BlockShade(_lineColorParamter.Value);
            Array.ForEach(_outline.WarpNoises, n => n.Scale = 0.01f);

            var fill = new NoiseGradient(new SKColor(60, 60, 60, 255), 0, 100);
            Array.ForEach(fill.NoiseFields, f => f.Scale = 0.001f);
            _bitmap.Fill(fill);

            _lineWeight = _lineWeightParameter.Value;
            _radius = 1;
        }
    }
}
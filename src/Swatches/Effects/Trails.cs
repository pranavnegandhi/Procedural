using Notadesigner.Shades;
using SkiaSharp;

namespace Notadesigner.Effects
{
    public class Trails : EffectBase
    {
        public override string ToString() => nameof(Trails);

        private readonly SKBitmap _bitmap;

        private readonly ColorParameter _colorParameter = new();

        private readonly NumericParameter<byte> _colorVarianceParameter = new();

        private readonly NumericParameter<byte> _warpSizeParameter = new();

        public Trails(SKBitmap bitmap)
        {
            _bitmap = bitmap;
            InitShade();
            _colorParameter.Value = new SKColor(255, 0, 0, 127);
            _colorParameter.Text = "Color";

            _colorVarianceParameter.MinValue = 1;
            _colorVarianceParameter.MaxValue = byte.MaxValue;
            _colorVarianceParameter.Value = 70;
            _colorVarianceParameter.Text = "Variance";

            _warpSizeParameter.MinValue = 1;
            _warpSizeParameter.MaxValue = byte.MaxValue;
            _warpSizeParameter.Value = 50;
            _warpSizeParameter.Text = "Warp Size";

            Parameters = new(_colorParameter, _colorVarianceParameter, _warpSizeParameter);
        }

        private int i = 1;

        private NoiseGradient ink;

        private void InitShade()
        {
            i = 1;

            var noiseFields = new NoiseField[]
            {
                new NoiseField(0.002f),
                new NoiseField(0.002f),
                new NoiseField(0.002f)
            };

            var warpNoises = new NoiseField[]
            {
                new NoiseField(0.003f),
                new NoiseField(0.003f),
                new NoiseField(0.003f)
            };

            ink = new NoiseGradient(_colorParameter.Value)
            {
                NoiseFields = noiseFields,
                ColorVariance = _colorVarianceParameter.Value,
                WarpSize = _warpSizeParameter.Value,
                WarpNoises = warpNoises
            };
        }

        public override void Execute()
        {
            ink.Color = _colorParameter.Value;

            _bitmap.CircleOutline(ink, new SKPoint(_bitmap.Width / 2, _bitmap.Height / 2), i);

            i += 4;
            ink.WarpSize += 5;
            if (i >= _bitmap.Width)
            {
                OnFinished();
            }
        }

        protected override void OnFinished()
        {
            InitShade();

            base.OnFinished();
        }

        public override void Reset()
        {
            _bitmap.Erase(SKColors.Black);
        }
    }
}
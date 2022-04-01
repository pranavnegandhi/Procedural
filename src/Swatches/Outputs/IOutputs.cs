using SkiaSharp;

namespace Notadesigner.Outputs
{
    public interface IOutput
    {
        void Write(SKBitmap canvas);
    }
}
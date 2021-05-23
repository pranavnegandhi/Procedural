using SkiaSharp;

namespace Desktop
{
    public interface IOutput
    {
        void Write(SKBitmap canvas);
    }
}
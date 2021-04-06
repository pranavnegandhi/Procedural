using SkiaSharp;

namespace Desktop
{
    public interface IOutput
    {
        void Write(SKSurface surface);
    }
}
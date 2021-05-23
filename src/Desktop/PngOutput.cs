using SkiaSharp;
using System.IO;

namespace Desktop
{
    public class PngOutput : IOutput
    {
        private delegate void WriterDelegate(SKBitmap canvas);

        private WriterDelegate _writer;

        private int _index;

        private string _path;

        public PngOutput()
        {
            _writer = Prepare;
        }

        public void Write(SKBitmap canvas)
        {
            _writer(canvas);
        }

        private void Prepare(SKBitmap canvas)
        {
            var root = Directory.GetCurrentDirectory();
            _path = Path.Combine(root, "output");
            var info = new DirectoryInfo(_path);
            if (!info.Exists)
            {
                info.Create();
            }

            _index = 0;
            _writer = WriteToFile;
            _writer(canvas);
        }

        private void WriteToFile(SKBitmap canvas)
        {
            _index++;

            using var image = SKImage.FromBitmap(canvas);
            var path = Path.Combine(_path, $"frame{_index:0000}.png");

            using var output = File.Create(path);
            var result = image.Encode(SKEncodedImageFormat.Png, 1);
            result.SaveTo(output);
        }
    }
}
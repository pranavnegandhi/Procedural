using SkiaSharp;
using System.IO;
using System.Reflection;

namespace Desktop
{
    public class PngOutput : IOutput
    {
        private delegate void WriterDelegate(SKSurface surface);

        private WriterDelegate _writer;

        private int _index;

        private string _path;

        public PngOutput()
        {
            _writer = Prepare;
        }

        public void Write(SKSurface surface)
        {
            _writer(surface);
        }

        private void Prepare(SKSurface surface)
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
            _writer(surface);
        }

        private void WriteToFile(SKSurface surface)
        {
            _index++;

            using (var image = surface.Snapshot())
            {
                var path = Path.Combine(_path, $"frame{_index:0000}.png");

                using (var output = File.Create(path))
                {
                    var result = image.Encode(SKEncodedImageFormat.Png, 1);
                    result.SaveTo(output);
                }
            }
        }
    }
}
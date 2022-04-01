using SkiaSharp;
using System.IO;

namespace Notadesigner.Outputs
{
    public class PngOutput : IOutput
    {
        private string _path;

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                var path = value;
                var fileInfo = new FileInfo(path);
                var dirInfo = fileInfo.Directory;

                if (null == dirInfo)
                {
                    var root = Directory.GetCurrentDirectory();
                    path = System.IO.Path.Combine(root, fileInfo.Name);
                }

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                _path = path;
            }
        }

        public void Write(SKBitmap canvas)
        {
            using var image = SKImage.FromBitmap(canvas);
            using var output = File.Create(Path);
            var result = image.Encode(SKEncodedImageFormat.Png, 1);
            result.SaveTo(output);
        }
    }
}
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Windows.Forms;

namespace Desktop
{
    public class ControlOutput : IOutput
    {
        private PictureBox _control;

        public ControlOutput(PictureBox control)
        {
            _control = control;
        }

        public void Write(SKSurface surface)
        {
            using (var image = surface.Snapshot())
            {
                var bitmap = SKBitmap.FromImage(image);
                _control.Image = bitmap.ToBitmap();
            }
        }
    }
}
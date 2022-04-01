using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing;
using System.Windows.Forms;

namespace Notadesigner.Outputs
{
    public class ControlOutput : IOutput
    {
        private readonly PictureBox _control;

        private Image _image;

        private bool _redraw = false;

        public ControlOutput(PictureBox control)
        {
            _control = control;
            _control.Paint += ControlPaintHandler;
        }

        public void Write(SKBitmap canvas)
        {
            _image = canvas.ToBitmap();
            _redraw = true;
            _control.Refresh();
        }

        private void ControlPaintHandler(object sender, PaintEventArgs e)
        {
            if (_redraw && _image != null)
            {
                var g = e.Graphics;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(_image,
                    (_control.Width - _image.Width) >> 1,
                    (_control.Height - _image.Height) >> 1,
                    _image.Width,
                    _image.Height);

                _redraw = false;
            }
        }
    }
}
using Notadesigner.Effects;
using SkiaSharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notadesigner.Apps.Views
{
    public class ColorParameterControl : ParameterControl<ParameterChangedEventArgs<SKColor>>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ColorParameter _parameter;

        private NumericParameterControl<byte> RedInput;

        private NumericParameterControl<byte> GreenInput;

        private NumericParameterControl<byte> BlueInput;

        private readonly UserControl _colorPreview = new();

        public ColorParameterControl()
        {
            _colorPreview.Location = new Point(100, 33);
            _colorPreview.Size = new Size(30, 145);
            _colorPreview.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(_colorPreview);

            Size = new Size(133, 200);
        }

        public ColorParameter Value
        {
            get
            {
                return _parameter;
            }

            set
            {
                _parameter = value;

                SuspendLayout();

                RedInput = _parameter.Red.CreateControl() as NumericParameterControl<byte>;
                RedInput.Location = new Point(3, 26);
                RedInput.Text = "Red";
                RedInput.Validated += new EventHandler(this.Parameter_Validated);

                GreenInput = _parameter.Green.CreateControl() as NumericParameterControl<byte>;
                GreenInput.Location = new Point(3, RedInput.Bottom);
                GreenInput.Text = "Green";
                GreenInput.Validated += new EventHandler(Parameter_Validated);

                BlueInput = _parameter.Blue.CreateControl() as NumericParameterControl<byte>;
                BlueInput.Location = new Point(3, GreenInput.Bottom);
                BlueInput.Text = "Blue";
                BlueInput.Validated += new EventHandler(Parameter_Validated);

                Controls.Add(RedInput);
                Controls.Add(GreenInput);
                Controls.Add(BlueInput);

                var red = RedInput.Value.Value;
                var green = GreenInput.Value.Value;
                var blue = BlueInput.Value.Value;
                byte alpha = 0xff;

                _colorPreview.BackColor = Color.FromArgb(alpha, red, green, blue);

                ResumeLayout();
            }
        }

        private void Parameter_Validated(object sender, EventArgs e)
        {
            var red = RedInput.Value.Value;
            var green = GreenInput.Value.Value;
            var blue = BlueInput.Value.Value;
            byte alpha = 0xff;
            var color = new SKColor(red, green, blue, alpha);
            var args = new ParameterChangedEventArgs<SKColor>(color);
            OnChange(args);

            _colorPreview.BackColor = Color.FromArgb(alpha, red, green, blue);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
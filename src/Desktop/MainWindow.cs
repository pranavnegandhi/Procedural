using Notadesigner.Shades;
using SkiaSharp;
using System;
using System.Windows.Forms;

namespace Desktop
{
    public partial class MainWindow : Form
    {
        private readonly Timer _screenTimer = new();

        private readonly SKImageInfo _info = new(800, 600);

        private readonly SKBitmap _canvas;

        private readonly AnimatedLines[] _lines;

        private readonly IOutput[] _outputModules = new IOutput[2];

        public MainWindow()
        {
            InitializeComponent();

            _canvas = Shade.Canvas(800, 600, new SKColor(255, 255, 255, 16));

            _outputModules[0] = new ControlOutput(pictureBox1);
            _outputModules[1] = new PngOutput();

            _lines = new AnimatedLines[]
            {
                new AnimatedLines(_info, _canvas),
                new AnimatedLines(_info, _canvas),
                new AnimatedLines(_info, _canvas),
                new AnimatedLines(_info, _canvas),
                new AnimatedLines(_info, _canvas)
            };

            _startButton.Click += Start;

            _lines[0].Initialize(new SKColor(0xff, 0xcc, 0x00, 0x0F));
            _lines[1].Initialize(new SKColor(0xff, 0x00, 0x00, 0x0F));
            _lines[2].Initialize(new SKColor(0x00, 0xff, 0x00, 0x0F));
            _lines[3].Initialize(new SKColor(0x00, 0x00, 0xff, 0x0F));
            _lines[4].Initialize(new SKColor(0xff, 0xcc, 0xff, 0x0F));

            _screenTimer.Interval = 1000 / 30;
            _screenTimer.Tick += (s, e) => DrawNextFrame();
        }

        private void Start(object sender, EventArgs e)
        {
            var shade = new BlockShade(new SKColor(0x00, 0x00, 0x00));
            shade.Fill(_canvas);

            _startButton.Enabled = false;
            Array.ForEach(_lines, l => l.Reset());
            _screenTimer.Start();
        }

        private void DrawNextFrame()
        {
            var completed = 0; // Set a bit for each line object that reaches its end

            for (var i = 0; i < _lines.Length; i++)
            {
                var l = _lines[i];
                var result = (l.DrawNextFrame() == -1) ? 1 : 0;
                completed |= result << i;
            }

            Array.ForEach(_outputModules, o => o.Write(_canvas));

            if (completed == 31)
            {
                _screenTimer.Stop();
                _startButton.Enabled = true;

                return;
            }
        }
    }
}
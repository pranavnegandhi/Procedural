using SkiaSharp;
using System;
using System.Windows.Forms;

namespace Desktop
{
    public partial class MainWindow : Form
    {
        private readonly Timer _screenTimer = new Timer();

        private readonly SKImageInfo _info = new SKImageInfo(800, 600);

        private readonly SKSurface _surface = SKSurface.Create(new SKImageInfo(800, 600));

        private readonly AnimatedLines[] _lines;

        private readonly IOutput[] _outputModules = new IOutput[2];

        public MainWindow()
        {
            InitializeComponent();
            _outputModules[0] = new ControlOutput(pictureBox1);
            _outputModules[1] = new PngOutput();

            _lines = new AnimatedLines[]
            {
                new AnimatedLines(_info, _surface),
                new AnimatedLines(_info, _surface),
                new AnimatedLines(_info, _surface),
                new AnimatedLines(_info, _surface),
                new AnimatedLines(_info, _surface)
            };

            _startButton.Click += Start;

            _lines[0].Initialize(new SKColor(0xff, 0xcc, 0x00));
            _lines[1].Initialize(new SKColor(0xff, 0x00, 0x00));
            _lines[2].Initialize(new SKColor(0x00, 0xff, 0x00));
            _lines[3].Initialize(new SKColor(0x00, 0x00, 0xff));
            _lines[4].Initialize(new SKColor(0xff, 0xcc, 0xff));

            _screenTimer.Interval = 1000 / 30;
            _screenTimer.Tick += (s, e) => DrawNextFrame();
        }

        private void Start(object sender, EventArgs e)
        {
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

            Array.ForEach(_outputModules, o => o.Write(_surface));

            if (completed == 31)
            {
                _screenTimer.Stop();
                _startButton.Enabled = true;

                return;
            }
        }
    }
}
using Notadesigner.Effects;
using Notadesigner.Outputs;
using Notadesigner.Shades;
using SkiaSharp;
using System;
using System.Windows.Forms;

namespace Notadesigner.Apps.Views
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// A collection of effect instances.
        /// </summary>
        private readonly EffectsOrganizer _effects;

        /// <summary>
        /// The active effect from the list.
        /// </summary>
        private IEffect _selectedEffect;

        /// <summary>
        /// The bitmap instance that is used to draw the output of the effect.
        /// All effects share the same bitmap and erase it before use.
        /// </summary>
        private readonly SKBitmap _bitmap;

        /// <summary>
        /// A timer used to invoke calls to <see cref="IEffect.Execute"/>. Once
        /// started, the timer continues to fire at fixed intervals until the
        /// effect dispatches a <see cref="IEffect.Finished"/> event. This mechanism
        /// makes it possible for animated effects to draw successive frames.
        /// Single frame effects plug into the same framework, but dispatch their
        /// <see cref="IEffect.Finished"/> event after a single invocation.
        /// </summary>
        private readonly Timer _screenTimer = new();

        /// <summary>
        /// A collection of modules that channel the results of the <see cref="IEffect"/>
        /// into an output destination such as an on-screen control or a file on disk.
        /// </summary>
        private readonly IOutput[] _outputModules = new IOutput[2];

        public MainWindow()
        {
            InitializeComponent();

            // Instantiate a ControlOutput to show the bitmap on the screen.
            _outputModules[0] = new ControlOutput(effectView);
            _outputModules[1] = new SequenceOutput(new PngOutput());

            _bitmap = Shade.Canvas(800, 800, new SKColor(0, 0, 0, 0));
            _effects = new EffectsOrganizer(_bitmap);

            effectsList.DataSource = _effects;
            effectsList.SelectedIndexChanged += EffectsListSelectedIndexChangedHandler;
            effectsList.SetSelected(0, true);

            // Wire up the screen timer.
            _screenTimer.Interval = 1000 / 30;
            _screenTimer.Tick += Execute;
        }

        /// <summary>
        /// Changes the active effect when the user makes a selection from the list.
        /// </summary>
        /// <param name="sender">The <see cref="ListView"/> control instance.</param>
        /// <param name="e">The event arguments.</param>
        private void EffectsListSelectedIndexChangedHandler(object sender, EventArgs e)
        {
            var currentEffect = _selectedEffect;
            if (currentEffect != null)
            {
                foreach (var p in currentEffect.Parameters)
                {
                    p.RemoveControl();
                }
            }

            _selectedEffect = effectsList.SelectedValue as IEffect;

            foreach (var p in _selectedEffect.Parameters)
            {
                var c = p.CreateControl();
                c.Margin = new Padding(3, 0, 10, 0);
                effectOptionsLayout.Controls.Add(c);
            }
        }

        /// <summary>
        /// Starts the timer that redraws the effect.
        /// </summary>
        /// <param name="sender">The <see cref="Button"/> control instance.</param>
        /// <param name="e">The event arguments.</param>
        private void StartButtonClickHandler(object sender, EventArgs e)
        {
            if (null == _selectedEffect)
            {
                return;
            }

            ((SequenceOutput)_outputModules[1]).Reset();
            startButton.Enabled = effectsList.Enabled = false;
            _selectedEffect.Finished += EffectFinished;
            _selectedEffect.Reset();
            _screenTimer.Start();
        }

        /// <summary>
        /// Fired by the timer at fixed intervals. Invokes the <see cref="IEffect.Execute"/>
        /// method which draws the output of the effect on the bitmap. The bitmap is
        /// then channeled to the output module.
        /// </summary>
        /// <param name="sender">The <see cref="Timer"/> instance running.</param>
        /// <param name="args">The event arguments.</param>
        private void Execute(object sender, EventArgs args)
        {
            _selectedEffect.Execute();

            Array.ForEach(_outputModules, m => m.Write(_bitmap));
        }

        /// <summary>
        /// Fired by the effect after it has completed its drawing operation.
        /// </summary>
        /// <param name="sender">The <see cref="IEffect"/> instance.</param>
        /// <param name="args">The event arguments. Reserved for future use.</param>
        private void EffectFinished(object sender, FinishedEventArgs args)
        {
            _screenTimer.Stop();
            _selectedEffect.Finished -= EffectFinished;
            startButton.Enabled = effectsList.Enabled = true;
        }

        /// <summary>
        /// Redraws the contents of the <see cref="PictureBox"/> if the window
        /// is resized so that its contents are centre-aligned to its new size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowResizeHandler(object sender, EventArgs e)
        {
            _outputModules[0].Write(_bitmap);
        }
    }
}
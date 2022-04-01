using SkiaSharp;

namespace Notadesigner.Outputs
{
    /// <summary>
    /// Generates a sequential collection of static images.
    /// Use the following commands to assemble them into a
    /// single .gif.
    ///
    /// Generate the palette.
    ///
    /// <code>ffmpeg -f image2 -framerate 5 -i output-%04d.png -filter_complex "[0:v] palettegen" palette.png</code>
    ///
    /// Assemble the frames into a single .gif.
    ///
    /// <code>ffmpeg -f lavfi -i nullsrc=s=800x600:duration=10:rate=25 \
    /// -i output-%04d.png -i palette.png \
    /// -filter_complex "[0:v][1:v] overlay[paused]; [paused][2:v] paletteuse=dither=bayer" \
    /// -y output.gif</code>
    /// 
    /// Assemble the frames into a single .mp4.
    /// 
    /// <code>ffmpeg -i output-%04d.png -c:v libx264 -vf "fps=120,format=yuv420p" out.mp4</code>
    /// 
    /// Add a pause at the end.
    /// 
    /// <code>ffmpeg -i out.mp4 -vf tpad=stop_mode=clone:stop_duration=2 output.mp4</code>
    /// </summary>
    public class SequenceOutput : IOutput
    {
        private readonly PngOutput _writer = new();

        private int _index = 0;

        public SequenceOutput(IOutput writer)
        {
            _writer = (PngOutput)writer;
        }

        public void Write(SKBitmap canvas)
        {
            _index++;
            _writer.Path = $"output\\output-{_index:0000}.png";
            _writer.Write(canvas);
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}
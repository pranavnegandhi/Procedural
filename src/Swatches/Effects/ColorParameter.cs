using Notadesigner.Apps.Views;
using SkiaSharp;
using System.Windows.Forms;

namespace Notadesigner.Effects
{
    public class ColorParameter : IParameter<SKColor>
    {
        private ColorParameterControl _control;

        private readonly NumericParameter<byte> _redParameter = new();

        private readonly NumericParameter<byte> _greenParameter = new();

        private readonly NumericParameter<byte> _blueParameter = new();

        private SKColor _value;

        public ColorParameter()
        {
            _redParameter.MaxValue = byte.MaxValue;
            _redParameter.MinValue = byte.MinValue;
            _greenParameter.MaxValue = byte.MaxValue;
            _greenParameter.MinValue = byte.MinValue;
            _blueParameter.MaxValue = byte.MaxValue;
            _blueParameter.MinValue = byte.MinValue;
        }

        public SKColor Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                _redParameter.Value = _value.Red;
                _greenParameter.Value = _value.Green;
                _blueParameter.Value = _value.Blue;
            }
        }

        public string Text
        {
            get;
            set;
        }

        public NumericParameter<byte> Red
        {
            get
            {
                return _redParameter;
            }
        }

        public NumericParameter<byte> Green
        {
            get
            {
                return _greenParameter;
            }
        }

        public NumericParameter<byte> Blue
        {
            get
            {
                return _blueParameter;
            }
        }

        public UserControl CreateControl()
        {
            _control = new ColorParameterControl()
            {
                Value = this,
                Text = Text
            };
            _control.Change += ControlChangeHandler;

            return _control;
        }

        public void RemoveControl()
        {
            var parent = _control.Parent;
            parent?.Controls?.Remove(_control);
        }

        private void ControlChangeHandler(object sender, ParameterChangedEventArgs<SKColor> e)
        {
            Value = e.Value;
        }
    }
}
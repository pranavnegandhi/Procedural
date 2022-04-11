using Notadesigner.Apps.Views;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Notadesigner.Effects
{
    public class NumericParameter<T> : IParameter<T> where T : struct, IComparable
    {
        private NumericParameterControl<T> _control;

        private T _value = default;

        private T _maxValue = default;

        private T _minValue = default;

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value.CompareTo(_maxValue) > 0 || value.CompareTo(_minValue) < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Value));
                }

                _value = value;
            }
        }

        public T MaxValue
        {
            get
            {
                return _maxValue;
            }

            set
            {
                _maxValue = value;
                if (_value.CompareTo(_maxValue) > 0)
                {
                    _value = _maxValue;
                }
            }
        }

        public T MinValue
        {
            get
            {
                return _minValue;
            }

            set
            {
                _minValue = value;
                if (_value.CompareTo(_minValue) < 0)
                {
                    _value = _minValue;
                }
            }
        }

        public string Text
        {
            get;
            set;
        }

        public UserControl CreateControl()
        {
            _control = new NumericParameterControl<T>()
            {
                Value = this,
                Text = Text
            };
            _control.Validating += ControlValidatingHandler;
            _control.Change += ControlChangeHandler;

            return _control;
        }

        public void RemoveControl()
        {
            var parent = _control.Parent;
            parent?.Controls?.Remove(_control);
        }

        private void ControlValidatingHandler(object sender, CancelEventArgs e)
        {
            if (e.GetType() != typeof(NumericParameterCancelEventArgs<T>))
            {
                return;
            }

            var args = (NumericParameterCancelEventArgs<T>)e;
            var value = args.Value;
            if (value.CompareTo(MaxValue) > 0 || value.CompareTo(MinValue) < 0)
            {
                e.Cancel = true;
            }
        }

        private void ControlChangeHandler(object sender, ParameterChangedEventArgs<T> e)
        {
            _value = e.Value;
        }
    }
}
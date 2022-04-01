using Notadesigner.Effects;
using System;
using System.ComponentModel;

namespace Notadesigner.Apps.Views
{
    public partial class NumericParameterControl<T> : ParameterControl<ParameterChangedEventArgs<T>> where T : struct, IComparable
    {
        private NumericParameter<T> _parameter;

        public NumericParameterControl()
        {
            InitializeComponent();
        }

        public NumericParameter<T> Value
        {
            get
            {
                return _parameter;
            }

            set
            {
                _parameter = value;
                ParameterInput.Maximum = Convert.ToDecimal(_parameter.MaxValue);
                ParameterInput.Minimum = Convert.ToDecimal(_parameter.MinValue);
                ParameterInput.Value = Convert.ToDecimal(_parameter.Value);
            }
        }

        private void Parameter_Validating(object sender, CancelEventArgs e)
        {
            var args = new NumericParameterCancelEventArgs<T>((T)Convert.ChangeType(ParameterInput.Value, typeof(T)));
            OnValidating(args);
            e.Cancel = args.Cancel;
        }

        private void Parameter_Validated(object sender, EventArgs e)
        {
            var args = new ParameterChangedEventArgs<T>((T)Convert.ChangeType(ParameterInput.Value, typeof(T)));
            OnChange(args);
        }
    }
}
using System;
using System.Windows.Forms;

namespace Notadesigner.Apps.Views
{
    public partial class ParameterControl<TChangeEventArgs> : UserControl where TChangeEventArgs : EventArgs
    {
        public event EventHandler<TChangeEventArgs> Change;

        public ParameterControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return ParameterLabel.Text;
            }

            set
            {
                ParameterLabel.Text = value;
            }
        }

        protected void OnChange(TChangeEventArgs args)
        {
            Change?.Invoke(this, args);
        }
    }
}
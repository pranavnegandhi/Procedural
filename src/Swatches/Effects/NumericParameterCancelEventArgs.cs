using System.ComponentModel;

namespace Notadesigner.Effects
{
    public class NumericParameterCancelEventArgs<T> : CancelEventArgs where T : struct
    {
        public NumericParameterCancelEventArgs(T value)
        {
            Value = value;
        }

        public T Value
        {
            get;
            set;
        }
    }
}
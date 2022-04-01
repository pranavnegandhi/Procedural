using System;

namespace Notadesigner.Effects
{
    public class ParameterChangedEventArgs<T> : EventArgs
    {
        public ParameterChangedEventArgs(T value)
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
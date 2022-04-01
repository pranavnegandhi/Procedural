using System;

namespace Notadesigner.Effects
{
    public class FinishedEventArgs : EventArgs
    {
        private static readonly FinishedEventArgs _empty = new();

        public static new FinishedEventArgs Empty
        {
            get => _empty;
        }
    }
}
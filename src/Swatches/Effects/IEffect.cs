using System;

namespace Notadesigner.Effects
{
    public interface IEffect : IComparable<IEffect>
    {
        public event EventHandler<FinishedEventArgs> Finished;

        public ParameterCollection Parameters
        {
            get;
        }

        public void Execute();

        public void Reset();

        public string ToString();
    }
}
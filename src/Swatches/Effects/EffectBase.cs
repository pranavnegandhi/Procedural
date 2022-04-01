using System;

namespace Notadesigner.Effects
{
    public abstract class EffectBase : IEffect
    {
        public virtual ParameterCollection Parameters
        {
            get;
            protected set;
        } = new();

        public event EventHandler<FinishedEventArgs> Finished;

        public int CompareTo(IEffect other)
        {
            return string.Compare(ToString(), other.ToString());
        }

        public abstract void Execute();

        public abstract void Reset();

        protected virtual void OnFinished()
        {
            Finished?.Invoke(this, FinishedEventArgs.Empty);
        }
    }
}
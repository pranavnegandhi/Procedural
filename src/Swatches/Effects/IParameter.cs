using System.Windows.Forms;

namespace Notadesigner.Effects
{
    public interface IParameter<T> : IParameter
    {
        public T Value
        {
            get;
            set;
        }
    }

    public interface IParameter
    {
        public string Text
        {
            get;
            set;
        }

        public UserControl CreateControl();

        public void RemoveControl();
    }
}
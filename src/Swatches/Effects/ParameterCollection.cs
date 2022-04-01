using System.Collections;
using System.Collections.Generic;

namespace Notadesigner.Effects
{
    public class ParameterCollection : IReadOnlyList<IParameter>
    {
        private readonly IList<IParameter> _parameters;

        public ParameterCollection(params IParameter[] parameters)
        {
            _parameters = new List<IParameter>(parameters);
        }

        public IParameter this[int index] => _parameters[index];

        public int Count => _parameters.Count;

        public IEnumerator<IParameter> GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Notadesigner.Utilities
{
    public class ComparisonComparer<T> : IComparer<T>, IComparer
    {
        private readonly Comparison<T> _comparison;

        public ComparisonComparer(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        public int Compare(T a, T b)
        {
            return _comparison(a, b);
        }

        public int Compare(object a, object b)
        {
            return _comparison((T)a, (T)b);
        }
    }
}
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Notadesigner.Effects
{
    public class EffectsOrganizer : IList
    {
        /// <summary>
        /// A collection of effect instances.
        /// </summary>
        private readonly List<IEffect> _effects = new();

        public EffectsOrganizer(SKBitmap bitmap)
        {
            // Prepare the list of effects.
            _effects.Add(new Procedural(bitmap, new SKColor(0xff, 0xcc, 0x00, 0xff)));
            _effects.Add(new RisingSun(bitmap));
            _effects.Add(new Carousel(bitmap));
            _effects.Add(new Circle(bitmap));
            _effects.Add(new Grid(bitmap));
            _effects.Add(new Trails(bitmap));
            _effects.Sort(new Comparison<IEffect>((a, b) => string.Compare(a.ToString(), b.ToString())));
        }

        public object this[int index]
        {
            get
            {
                return _effects[index];
            }

            set
            {
                _effects[index] = value as IEffect;
            }
        }

        public int Count => _effects.Count;

        public bool IsReadOnly => true;

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public int Add(object? item)
        {
            _effects.Add(item as IEffect);

            return _effects.Count;
        }

        public void Clear()
        {
            _effects.Clear();
        }

        public bool Contains(object item)
        {
            return _effects.Contains(item as IEffect);
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return _effects.GetEnumerator();
        }

        public int IndexOf(object? item)
        {
            return _effects.IndexOf(item as IEffect);
        }

        public void Insert(int index, object item)
        {
            _effects.Insert(index, item as IEffect);
        }

        public void Remove(object? item)
        {
            _effects.Remove(item as IEffect);
        }

        public void RemoveAt(int index)
        {
            _effects.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _effects.GetEnumerator();
        }
    }
}
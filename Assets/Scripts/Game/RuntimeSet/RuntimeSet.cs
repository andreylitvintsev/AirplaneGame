using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.RuntimeSet
{
    public class RuntimeSet<T> : ScriptableObject, ISet<T>
    {
        private readonly ISet<T> _set = new HashSet<T>();

        public event Action Changed;

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _set.Add(item);
            Changed?.Invoke();
        }

        public void Clear()
        {
            _set.Clear();
            Changed?.Invoke();
        }

        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (_set.Remove(item))
            {
                Changed?.Invoke();
                return true;
            }
            return false;
        }

        public int Count => _set.Count;

        public bool IsReadOnly => _set.IsReadOnly;

        public bool Add(T item)
        {
            if (_set.Add(item))
            {
                Changed?.Invoke();
                return true;
            }
            return false;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            _set.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            _set.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _set.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _set.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _set.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            _set.UnionWith(other);
        }
    }
}

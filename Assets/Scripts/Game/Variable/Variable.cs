using UnityEngine;

namespace Game.Variable
{
    public abstract class Variable<T> : ScriptableObject
    {
        [SerializeField] private T _value = default;

        public T Value => _value;
    }
}
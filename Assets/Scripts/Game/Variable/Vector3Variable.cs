using UnityEngine;

namespace Game.Variable
{
    [CreateAssetMenu]
    public class Vector3Variable : Variable<Vector3>
    {
        public Bounds ToBounds(Vector3Variable includedPoint)
        {
            var bounds = new Bounds();
            bounds.Encapsulate(Value);
            bounds.Encapsulate(includedPoint.Value);
            return bounds;
        }
    }
}
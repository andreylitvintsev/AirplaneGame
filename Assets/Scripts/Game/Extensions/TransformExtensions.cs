using UnityEngine;

namespace Game.Extensions
{
    public static class TransformExtensions
    {
        public static void SetEuler(this Transform quaternion, float? x = null, float? y = null, float? z = null)
        {
            var newEulerAngles = quaternion.eulerAngles;
            
            newEulerAngles.x = x ?? newEulerAngles.x;
            newEulerAngles.y = y ?? newEulerAngles.y;
            newEulerAngles.z = z ?? newEulerAngles.z;

            quaternion.eulerAngles = newEulerAngles;
        }
    }
}
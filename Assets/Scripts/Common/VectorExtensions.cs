using UnityEngine;

namespace Common
{
    public static class VectorExtensions
    {
        public static Vector3 RotateByEulerX(this Vector3 vector3, float x)
        {
            return Quaternion.Euler(x, 0f, 0f) * vector3;
        }
        
        public static Vector3 RotateByEulerY(this Vector3 vector3, float y)
        {
            return Quaternion.Euler(0f, y, 0f) * vector3;
        }

        public static Vector3 RotateByEulerZ(this Vector3 vector3, float z)
        {
            return Quaternion.Euler(0f, 0f, z) * vector3;
        }
    }
}
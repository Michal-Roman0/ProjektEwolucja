using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 FromPolar(this Vector2 vector, float radius, float angle)
        {
            vector.Set(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
            return vector;
        }
    }
}
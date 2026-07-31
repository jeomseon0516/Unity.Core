using UnityEngine;

namespace Jeomseon.Mathematics
{
    public static class Vector2IntExtensions
    {
        public static Vector2Int Abs(this Vector2Int vec) => new(
            Mathf.Abs(vec.x),
            Mathf.Abs(vec.y));
    }
}

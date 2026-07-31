using UnityEngine;

namespace Jeomseon.Mathematics
{
    public static class Vector2Extensions
    {
        public static Vector2 Abs(this Vector2 vec) => new(
            Mathf.Abs(vec.x), 
            Mathf.Abs(vec.y));
    }
}

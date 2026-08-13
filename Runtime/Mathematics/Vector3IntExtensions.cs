using UnityEngine;

namespace Jeomseon.Unity.Core.Mathematics
{
    public static class Vector3IntExtensions
    {
        public static Vector3Int Abs(this Vector3Int vec) => new(
            Mathf.Abs(vec.x),
            Mathf.Abs(vec.y),
            Mathf.Abs(vec.z));
    }
}

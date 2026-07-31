using UnityEngine;

namespace Jeomseon.GameObjects
{
    public static class TransformExtensions
    {
        public static void SetPositionX(this Transform transform, float value)
        {
            Vector3 position = transform.position;
            position.x = value;
            transform.position = position;
        }

        public static void SetPositionY(this Transform transform, float value)
        {
            Vector3 position = transform.position;
            position.y = value;
            transform.position = position;
        }

        public static void SetPositionZ(this Transform transform, float value)
        {
            Vector3 position = transform.position;
            position.z = value;
            transform.position = position;
        }

        public static void SetLocalPositionX(this Transform transform, float value)
        {
            Vector3 position = transform.localPosition;
            position.x = value;
            transform.localPosition = position;
        }

        public static void SetLocalPositionY(this Transform transform, float value)
        {
            Vector3 position = transform.localPosition;
            position.y = value;
            transform.localPosition = position;
        }

        public static void SetLocalPositionZ(this Transform transform, float value)
        {
            Vector3 position = transform.localPosition;
            position.z = value;
            transform.localPosition = position;
        }

        public static void SetLocalScaleX(this Transform transform, float value)
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            transform.localScale = scale;
        }

        public static void SetLocalScaleY(this Transform transform, float value)
        {
            Vector3 scale = transform.localScale;
            scale.y = value;
            transform.localScale = scale;
        }

        public static void SetLocalScaleZ(this Transform transform, float value)
        {
            Vector3 scale = transform.localScale;
            scale.z = value;
            transform.localScale = scale;
        }
    }
}

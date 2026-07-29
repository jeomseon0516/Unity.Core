using System;

namespace Jeomseon.Helper
{
    // TODO(리팩토링): NumericExtensions와 기능이 중복되므로 하나의 공개 API로 통합하고,
    // Unity Mathf 및 System.Math.Clamp로 대체 가능한 타입 범위를 검토해야 합니다.
    public static class NumericsHelper
    {
        // -----------------------------
        // Min
        // -----------------------------
        public static T Min<T>(T value, T min) where T : IComparable<T>
        {
            return value.CompareTo(min) < 0 ? min : value;
        }

        // -----------------------------
        // Max
        // -----------------------------
        public static T Max<T>(T value, T max) where T : IComparable<T>
        {
            return value.CompareTo(max) > 0 ? max : value;
        }

        // -----------------------------
        // Clamp
        // -----------------------------
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }
    }
}

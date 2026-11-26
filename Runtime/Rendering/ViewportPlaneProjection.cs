using System;
using UnityEngine;

namespace Jeomseon.Unity.Core.Rendering
{
    /// <summary>
    /// 카메라의 정규화된 뷰포트 모서리를 월드 평면으로 투영합니다.
    /// </summary>
    public static class ViewportPlaneProjection
    {
        /// <summary>
        /// 네 모서리의 교점을 좌하단, 우하단, 좌상단, 우상단 순으로 기록합니다.
        /// </summary>
        /// <remarks>
        /// 한 광선이라도 시작점 앞에서 평면을 만나지 못하면 false를 반환하며 결과를 변경하지 않습니다.
        /// 광선은 카메라의 근거리 클리핑 평면에서 시작하며 원거리 클리핑 평면까지의 거리는 검사하지 않습니다.
        /// </remarks>
        /// <exception cref="ArgumentNullException">카메라가 없거나 파괴되었습니다.</exception>
        /// <exception cref="ArgumentException">결과 버퍼가 네 칸보다 작습니다.</exception>
        public static bool TryProjectCorners(Camera camera, Plane plane, Span<Vector3> corners)
        {
            if (!camera)
                throw new ArgumentNullException(nameof(camera));
            if (corners.Length < 4)
                throw new ArgumentException("At least four slots are required.", nameof(corners));

            Span<Vector3> intersections = stackalloc Vector3[4];
            for (var index = 0; index < intersections.Length; index++)
            {
                var ray = camera.ViewportPointToRay(new Vector3(index % 2, index / 2, 0f));
                if (!plane.Raycast(ray, out var distance))
                    return false;

                intersections[index] = ray.GetPoint(distance);
            }

            intersections.CopyTo(corners);
            return true;
        }
    }
}

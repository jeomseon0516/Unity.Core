using System;
using Jeomseon.Unity.Core.Rendering;
using UnityEngine;

namespace Jeomseon.Unity.Core.Grids
{
    /// <summary>
    /// 카메라 뷰포트가 덮는 직사각형 XY 그리드의 셀 경계를 계산합니다.
    /// </summary>
    public static class ViewportCellBounds
    {
        /// <summary>
        /// 그리드 로컬 XY 평면의 교점들을 덮는 반개구간 셀 경계 [min, max)를 계산합니다.
        /// 결과는 행과 열에 정렬된 사각형이며 Z 범위는 [0, 1)입니다.
        /// </summary>
        /// <remarks>
        /// Rectangle 레이아웃과 XYZ 셀 축 순서를 지원합니다. 지원하지 않는 레이아웃, 교차 실패 또는
        /// BoundsInt 범위 초과 시 false와 기본 경계를 반환합니다. 원거리 클리핑과 가림은 계산에 포함하지 않습니다.
        /// </remarks>
        /// <exception cref="ArgumentNullException">카메라 또는 그리드가 없거나 파괴되었습니다.</exception>
        public static bool TryCalculate(Camera camera, GridLayout grid, out BoundsInt bounds)
        {
            if (!camera)
                throw new ArgumentNullException(nameof(camera));
            if (!grid)
                throw new ArgumentNullException(nameof(grid));

            bounds = default;
            if (grid.cellLayout != GridLayout.CellLayout.Rectangle ||
                grid.cellSwizzle != GridLayout.CellSwizzle.XYZ)
                return false;

            var transform = grid.transform;
            var plane = new Plane(transform.position, transform.TransformPoint(Vector3.right),
                transform.TransformPoint(Vector3.up));
            Span<Vector3> corners = stackalloc Vector3[4];
            if (!ViewportPlaneProjection.TryProjectCorners(camera, plane, corners))
                return false;

            var min = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            var max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
            foreach (var corner in corners)
            {
                var cell = grid.LocalToCellInterpolated(transform.InverseTransformPoint(corner));
                if (!float.IsFinite(cell.x) || !float.IsFinite(cell.y))
                    return false;

                var xy = new Vector2(cell.x, cell.y);
                min = Vector2.Min(min, xy);
                max = Vector2.Max(max, xy);
            }

            if (!TryRoundRange(min.x, max.x, out var xMin, out var width) ||
                !TryRoundRange(min.y, max.y, out var yMin, out var height))
                return false;

            bounds = new BoundsInt(xMin, yMin, 0, width, height, 1);
            return true;
        }

        private static bool TryRoundRange(float min, float max, out int start, out int size)
        {
            var lower = Math.Floor(min);
            var upper = Math.Ceiling(max);
            var length = upper - lower;
            start = 0;
            size = 0;

            if (lower < int.MinValue || upper > int.MaxValue || length > int.MaxValue)
                return false;

            start = (int)lower;
            size = (int)length;
            return true;
        }
    }
}

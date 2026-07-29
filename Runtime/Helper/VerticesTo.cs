using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jeomseon.Helper
{
    // TODO(모듈화): Renderer/메시 경계 계산은 Core에서 분리할지 검토하고,
    // 매 호출 GetComponentsInChildren과 배열 할당을 줄이는 API 및 로컬/월드 좌표 계약을 정의해야 합니다.
    // 클래스 이름도 계산 결과가 드러나는 이름으로 변경해야 합니다.
    public static class VerticesTo
    {
        public static float GetHeightFromVertices(GameObject obj)
        {
            return GetTotalSize(obj).y;
        }

        public static float GetZWidthFromVertices(GameObject obj)
        {
            return GetTotalSize(obj).z;
        }

        public static float GetXWidthFromVertices(GameObject obj)
        {
            return GetTotalSize(obj).x;
        }

        public static Vector3 GetMinPoint(GameObject obj)
        {
            Renderer[] renderers = obj
                .GetComponentsInChildren<Renderer>()
                .Where(renderer => renderer is not ParticleSystemRenderer)
                .ToArray();

            if (renderers.Length == 0)
            {
                Debug.Log("vertices not found!");
                return Vector3.zero;
            }

            Bounds totalBounds = renderers[0].bounds;

            foreach (Renderer renderer in renderers)
            {
                totalBounds.Encapsulate(renderer.bounds);
            }

            return totalBounds.min;
        }

        public static Vector3 GetTotalSize(GameObject obj)
        {
            Renderer[] renderers = obj
                .GetComponentsInChildren<Renderer>()
                .Where(renderer => renderer is not ParticleSystemRenderer)
                .ToArray();

            if (renderers.Length == 0)
            {
                Debug.Log("vertices not found!");
                return Vector3.zero;
            }

            Bounds totalBounds = renderers[0].bounds;

            foreach (Renderer renderer in renderers)
            {
                totalBounds.Encapsulate(renderer.bounds);
            }

            return totalBounds.size;
        }
    }
}

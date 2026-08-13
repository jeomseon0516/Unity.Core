using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jeomseon.Unity.Core.Rendering
{
    /// <summary>
    /// GameObject 계층에 포함된 Renderer의 월드 공간 경계를 계산합니다.
    /// </summary>
    public static class RendererBoundsCalculator
    {
        /// <summary>
        /// 자식 Renderer의 월드 공간 경계를 결합합니다.
        /// 효과의 일시적인 크기에 영향을 받는 ParticleSystemRenderer는 제외합니다.
        /// </summary>
        public static bool TryCalculateWorldBounds(
            GameObject root,
            out Bounds bounds,
            bool includeInactive = false)
        {
            if (!root)
                throw new ArgumentNullException(nameof(root));

            List<Renderer> renderers = new();
            root.GetComponentsInChildren(includeInactive, renderers);

            bounds = default;
            bool hasBounds = false;

            foreach (Renderer renderer in renderers)
            {
                if (renderer is ParticleSystemRenderer)
                    continue;

                if (!hasBounds)
                {
                    bounds = renderer.bounds;
                    hasBounds = true;
                    continue;
                }

                bounds.Encapsulate(renderer.bounds);
            }

            return hasBounds;
        }
    }
}

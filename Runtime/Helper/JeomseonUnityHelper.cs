using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jeomseon.Helper
{
    // TODO(리팩토링): Destroy/DestroyImmediate 래퍼가 제공하는 실제 이점을 검증하고,
    // Play Mode와 Edit Mode에 따른 파괴 정책 및 ref 매개변수 사용 규칙을 정리해야 합니다.
    public static class JeomseonUnityHelper 
    {
        public static void DestroyObject<T>(ref T @object) where T : Object
        {
            if (!@object) return;

            Object.Destroy(@object);
            @object = null;
        }

        public static void DestroyObject(Object @object)
        {
            if (!@object) return;

            Object.Destroy(@object);
        }

        public static void DestroyObjectByComponent<T>(T component) where T : Component
        {
            if (!component) return;

            DestroyObject(component.gameObject);
        }

        /// <summary>
        /// .. component를 참조로 가져와서 Destroy후 Null초기화를 합니다
        /// </summary>
        /// <param name="component"></param>
        public static void DestroyObjectByComponent<T>(ref T component) where T : Component
        {
            if (!component) return;

            DestroyObject(component.gameObject);
            component = null;
        }

        public static void DestroyImmediateObject<T>(ref T @object) where T : Object
        {
            if (!@object) return;

            Object.DestroyImmediate(@object);
            @object = null;
        }

        public static void DestroyImmediateObject(Object @object)
        {
            if (!@object) return;

            Object.DestroyImmediate(@object);
        }

        public static void DestroyImmediateComponent<T>(ref T component) where T : Component
        {
            if (!component) return;

            Object.DestroyImmediate(component);
            component = null;
        }

        public static void DestroyImmediateObjectByComponent<T>(T component) where T : Component
        {
            if (!component) return;

            DestroyImmediateObject(component.gameObject);
        }

        /// <summary>
        /// .. component를 참조로 가져와서 Destroy후 Null초기화를 합니다
        /// </summary>
        /// <param name="component"></param>
        public static void DestroyImmediateObjectByComponent<T>(ref T component) where T : Component
        {
            if (!component) return;

            DestroyImmediateObject(component.gameObject);
            component = null;
        }

        public static void ReleaseRenderTexture(RenderTexture renderTexture)
        {
            if (!renderTexture) return;

            renderTexture.Release();
        }

        /// <summary>
        /// .. RenderTexture를 참조로 가져와서 Release후 Null초기화를 합니다
        /// </summary>
        /// <param name="renderTexture"></param>
        public static void ReleaseRenderTexture(ref RenderTexture renderTexture)
        {
            if (!renderTexture) return;

            renderTexture.Release();
            renderTexture = null;
        }
    }
}

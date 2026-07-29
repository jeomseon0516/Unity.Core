using Jeomseon.Collections;
using UnityEngine;

namespace Jeomseon.Samples.Core
{
    public sealed class CoreCollectionsSample : MonoBehaviour
    {
        [ContextMenu("Deque 예제 실행")]
        private void Run()
        {
            Deque<string> messages = new();
            messages.AddFirst("first");
            messages.AddLast("last");
            Debug.Log($"{messages.DequeueFirst()} / {messages.DequeueLast()}");
        }
    }
}

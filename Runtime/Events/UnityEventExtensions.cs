using UnityEngine.Events;

namespace Jeomseon.Events
{
    public static class UnityEventExtensions
    {
        public static void SetPersistentListenerState(this UnityEventBase unityEvent, UnityEventCallState eventState)
        {
            for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                unityEvent.SetPersistentListenerState(i, eventState);
            }
        }
    }
}

using UnityEngine.UIElements;

namespace Jeomseon.UIElements
{
    public static class VisualElementExtensions
    {
        public static VisualElement GetRootVisualElement(this VisualElement visualElement)
        {
            VisualElement current = visualElement;

            while (current?.parent is not null)
            {
                current = current.parent;
            }

            return current;
        }
    }
}

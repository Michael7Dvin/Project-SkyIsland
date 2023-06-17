using System;
using UnityEngine.EventSystems;

namespace UI.Elements.EventsNotifier
{
    public interface IUIElementEventsNotifier
    {
        public event Action<PointerEventData> PointerEntered;
        public event Action<PointerEventData> PointerExited;
        public event Action<PointerEventData> PointerDowned;
        public event Action<PointerEventData> PointerUpped;
        public event Action<PointerEventData> PointerClicked;
    }
}
using System;
using UI.Elements.EventsNotifier;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements.Buttons
{
    [RequireComponent(typeof(IUIElementEventsNotifier))]
    public class BaseButton : MonoBehaviour
    {
        private void Awake() => 
            Events = GetComponent<IUIElementEventsNotifier>();

        protected virtual void OnEnable() => 
            Events.PointerClicked += OnClicked;

        protected virtual  void OnDisable() => 
            Events.PointerClicked -= OnClicked;

        protected IUIElementEventsNotifier Events { get; private set; }
        
        public event Action Cliked;

        private void OnClicked(PointerEventData eventData) => 
            Cliked?.Invoke();
    }
}